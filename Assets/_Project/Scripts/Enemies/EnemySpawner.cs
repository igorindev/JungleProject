using Collection;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemySpawner
{
    void Setup(IGameRound gameRound, IPlayerEconomy playerEconomy, IScore score);
    void StartGame();
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

    int maxEnemiesThisRound;
    int spawnedEnemiesCount;
    int killedEnemiesCount;

    IEnemyInstantiator enemyInstantiator;
    IGameRound _gameRound;
    IPlayerEconomy _playerEconomy;
    IScore _score;


    readonly List<Enemy> spawnedEnemies = new List<Enemy>();

    public void Setup(IGameRound gameRound, IPlayerEconomy playerEconomy, IScore score)
    {
        _score = score;
        _playerEconomy = playerEconomy;
        _gameRound = gameRound;
        enemyInstantiator = new EnemyInstantiator(_enemyCollection, _spawnRadius, _fixedYSpawnPosition);
    }

    void Update()
    {
        _spawnDelaycounter += Time.deltaTime;
        if (spawnedEnemiesCount < maxEnemiesThisRound && _spawnDelaycounter > _baseSpawnPerSecond)
        {
            _spawnDelaycounter -= _baseSpawnPerSecond;
            int enemyIndex = _enemyCollection.GetRandomIndexCollection();
            Enemy enemyInstance = enemyInstantiator.Spawn(enemyIndex);
            enemyInstance.Setup(_enemyCollection.GetFromCollection(enemyIndex));
            if (enemyInstance.TryGetComponent(out IHealth health))
            {
                health.OnDie += OnEnemyKilled;
                health.OnDestroyGO += EnemyDestroyed;
            }

            spawnedEnemiesCount++;
            spawnedEnemies.Add(enemyInstance);
        }
    }

    void OnEnemyKilled()
    {
        _score.AddScore(5);
        _playerEconomy.AddCoins(5);
    }

    void EnemyDestroyed()
    {
        killedEnemiesCount++;
        CheckIfRoundEnded();
    }

    void CheckIfRoundEnded()
    {
        if (killedEnemiesCount == maxEnemiesThisRound)
        {
            _spawnDelaycounter = 0;
            killedEnemiesCount = 0;
            spawnedEnemiesCount = 0;
            maxEnemiesThisRound = _gameRound.NewRound();
        }
    }

    public void StartGame()
    {
        CheckIfRoundEnded();
    }
}