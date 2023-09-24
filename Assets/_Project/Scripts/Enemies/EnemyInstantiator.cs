using Collection;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyInstantiator
{
    Enemy Spawn(int enemyReference);
}

public class EnemyInstantiator : IEnemyInstantiator
{
    readonly float _spawnRadius;
    readonly float _fixedYSpawnPosition;
    readonly Vector3 _target;

    const float kCircle = 2 * Mathf.PI * Mathf.Deg2Rad;

    readonly List<EnemyPool> _pools = new List<EnemyPool>();

    public EnemyInstantiator(EnemyCollection enemyCollection, float spawnRadius, float fixedYSpawnPosition)
    {
        _spawnRadius = spawnRadius;
        _fixedYSpawnPosition = fixedYSpawnPosition;
        _target = new Vector3(0, fixedYSpawnPosition, 0);

        for (int i = 0; i < enemyCollection.GetSize(); i++)
        {
            _pools.Add(new EnemyPool(enemyCollection.GetFromCollection(i).Prefab));
        }
    }

    public Enemy Spawn(int enemyReference)
    {
        Vector3 pos = CalculatePosition();
        Quaternion rot = CalculateRotation(pos);
        Enemy e = _pools[enemyReference].Pool.Get();
        e.transform.SetPositionAndRotation(pos, rot);
        return e;
    }

    Vector3 CalculatePosition()
    {
        float angle = kCircle * Random.Range(0, 360);

        return new Vector3(_spawnRadius * Mathf.Cos(angle), _fixedYSpawnPosition, _spawnRadius * Mathf.Sin(angle));
    }

    Quaternion CalculateRotation(Vector3 pos)
    {
        Vector3 result = (_target - pos).normalized;

        return Quaternion.LookRotation(result);
    }
}