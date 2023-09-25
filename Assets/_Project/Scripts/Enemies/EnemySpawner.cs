using Collection;
using UnityEngine;

public interface IEnemySpawner
{
    void Setup(IGameRound gameRound, IPlayerEconomy playerEconomy, IScore score);
    void StartSpawn();
}

public class EnemySpawner : MonoBehaviour, IEnemySpawner
{
    [Header("ENEMY PREFAB")]
    [SerializeField] EnemyCollection _enemyCollection;

    [Header("SPAWN AREA CONFIG")]
    [SerializeField] float _spawnRadius = 2;
    [SerializeField] float _fixedYSpawnPosition = 0.125f;
    [SerializeField] AnimationCurve _spawnRateOverTime;

    readonly float _baseSpawnPerSecond = 1f;
    float _spawnDelayCounter;

    int _maxEnemiesThisRound;
    int _spawnedEnemiesCount;
    int _killedEnemiesCount;

    IEnemyInstantiator _enemyInstantiator;
    IGameRound _gameRound;
    IPlayerEconomy _playerEconomy;
    IScore _score;

    public float BaseSpawnPerSecond { get => _baseSpawnPerSecond / _gameRound.GetCurrentRound(); }

    bool _spawnBoss;

    public void Setup(IGameRound gameRound, IPlayerEconomy playerEconomy, IScore score)
    {
        _score = score;
        _playerEconomy = playerEconomy;
        _gameRound = gameRound;
        _enemyInstantiator = new EnemyInstantiator(_enemyCollection, _spawnRadius, _fixedYSpawnPosition);
    }

    public void StartSpawn()
    {
        CheckIfAllEnemiesKilled();
    }

    void Update()
    {
        _spawnDelayCounter += Time.deltaTime;
        if (_spawnedEnemiesCount < _maxEnemiesThisRound && _spawnDelayCounter > BaseSpawnPerSecond)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        _spawnDelayCounter -= BaseSpawnPerSecond;
        
        int enemyIndex = Random.Range(0, 100f) > 25f ? 0 : 1;
        if (_spawnBoss)
        {
            _spawnBoss = false;
            enemyIndex = 2;
        }

        Enemy enemyInstance = _enemyInstantiator.Spawn(enemyIndex);
        enemyInstance.Setup(_enemyCollection.GetFromCollection(enemyIndex), _gameRound.GetCurrentRound());
        if (enemyInstance.TryGetComponent(out IHealth health))
        {
            health.OnDie += OnEnemyKilled;
            health.OnDestroyGO += EnemyDestroyed;
        }

        _spawnedEnemiesCount++;
    }

    void OnEnemyKilled()
    {
        _score.AddScore(5);
        _playerEconomy.AddCoins(5);
    }

    void EnemyDestroyed()
    {
        _killedEnemiesCount++;
        CheckIfAllEnemiesKilled();
    }

    void CheckIfAllEnemiesKilled()
    {
        if (_killedEnemiesCount == _maxEnemiesThisRound)
        {
            _spawnDelayCounter = 0;
            _killedEnemiesCount = 0;
            _spawnedEnemiesCount = 0;
            _maxEnemiesThisRound = Mathf.Clamp(_gameRound.NewRound(), 0, 60);
            _spawnBoss = _gameRound.GetCurrentRound() % 5 == 0;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, _spawnRadius);
    }
}