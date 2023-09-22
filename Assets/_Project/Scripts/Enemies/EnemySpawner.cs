using Collection;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemySpawner
{
    void Setup(IGameRound gameRound, IPlayerEconomy playerEconomy);
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
    float _counter;

    int maxEnemiesThisRound;
    int spawnedEnemiesCount;
    int killedEnemiesCount;

    IEnemyInstantiator enemyInstantiator;
    IGameRound _gameRound;
    IPlayerEconomy _playerEconomy;

    List<Enemy> spawnedEnemies = new List<Enemy>();

    public void Setup(IGameRound gameRound, IPlayerEconomy playerEconomy)
    {
        _playerEconomy = playerEconomy;
        _gameRound = gameRound;
        enemyInstantiator = new EnemyInstantiator(_spawnRadius, _fixedYSpawnPosition);
    }

    void Update()
    {
        _counter += Time.deltaTime;
        if (spawnedEnemiesCount < maxEnemiesThisRound && _counter > _baseSpawnPerSecond)
        {
            _counter--;
            EnemyData enemyData = _enemyCollection.GetRandomFromCollection();
            Enemy enemyInstance = enemyInstantiator.Spawn(_enemyCollection.GetRandomFromCollection().Prefab);
            enemyInstance.Setup(enemyData, OnEnemyKilled);

            spawnedEnemiesCount++;
            spawnedEnemies.Add(enemyInstance);
        }
    }

    int GetSpawnRate()
    {
        float spawnRate = _spawnRateOverTime.Evaluate(Time.time);
        return (int)spawnRate;
    }

    void OnEnemyKilled()
    {
        _playerEconomy.AddCoins(5);
        killedEnemiesCount++;
        CheckIfRoundEnded();
    }

    void CheckIfRoundEnded()
    {
        if (killedEnemiesCount == maxEnemiesThisRound)
        {
            _counter = 0;
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