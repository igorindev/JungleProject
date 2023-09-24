using Collection;
using System.Collections.Generic;
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

    float _baseSpawnPerSecond = 1f; //1/s
    float _spawnDelaycounter;

    int _maxEnemiesThisRound;
    int _spawnedEnemiesCount;
    int _killedEnemiesCount;

    IEnemyInstantiator _enemyInstantiator;
    IGameRound _gameRound;
    IPlayerEconomy _playerEconomy;
    IScore _score;

    readonly List<Enemy> spawnedEnemies = new List<Enemy>();

    public float BaseSpawnPerSecond { get => _baseSpawnPerSecond / _gameRound.GetCurrentRound(); }

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
        _spawnDelaycounter += Time.deltaTime;
        if (_spawnedEnemiesCount < _maxEnemiesThisRound && _spawnDelaycounter > BaseSpawnPerSecond)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        _spawnDelaycounter -= BaseSpawnPerSecond;
        int enemyIndex = _enemyCollection.GetRandomIndexCollection();
        Enemy enemyInstance = _enemyInstantiator.Spawn(enemyIndex);
        enemyInstance.Setup(_enemyCollection.GetFromCollection(enemyIndex));
        if (enemyInstance.TryGetComponent(out IHealth health))
        {
            health.OnDie += OnEnemyKilled;
            health.OnDestroyGO += EnemyDestroyed;
        }

        _spawnedEnemiesCount++;
        spawnedEnemies.Add(enemyInstance);
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
            _spawnDelaycounter = 0;
            _killedEnemiesCount = 0;
            _spawnedEnemiesCount = 0;
            _maxEnemiesThisRound = _gameRound.NewRound();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, _spawnRadius);
    }
}