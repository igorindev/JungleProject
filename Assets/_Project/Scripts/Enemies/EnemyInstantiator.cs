using UnityEngine;

public interface IEnemyInstantiator
{
    Enemy Spawn(Enemy enemy);
}

public class EnemyInstantiator : IEnemyInstantiator
{
    readonly float _spawnRadius;
    readonly float _fixedYSpawnPosition;
    readonly Vector3 target;

    const float kCircle = 2 * Mathf.PI * Mathf.Deg2Rad;

    public EnemyInstantiator(float spawnRadius, float fixedYSpawnPosition)
    {
        _spawnRadius = spawnRadius;
        _fixedYSpawnPosition = fixedYSpawnPosition;
        target = new Vector3(0, fixedYSpawnPosition, 0);
    }

    public Enemy Spawn(Enemy enemyReference)
    {
        Vector3 pos = CalculatePosition();
        Quaternion rot = CalculateRotation(pos);

        return Object.Instantiate(enemyReference, pos, rot);
    }

    Vector3 CalculatePosition()
    {
        float angle = kCircle * Random.Range(0, 360);

        return new Vector3(_spawnRadius * Mathf.Cos(angle), _fixedYSpawnPosition, _spawnRadius * Mathf.Sin(angle));
    }

    Quaternion CalculateRotation(Vector3 pos)
    {
        Vector3 result = (target - pos).normalized;

        return Quaternion.LookRotation(result);
    }
}