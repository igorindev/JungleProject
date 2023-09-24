using UnityEngine;

public class EnemyPool : PoolBase<Enemy>
{
    public EnemyPool(Enemy prefab) : base(prefab) { }

    protected override Enemy CreatePooledItem()
    {
        Enemy component = Object.Instantiate(_prefab);

        // This is used to return ParticleSystems to the pool when they have stopped.
        ReturnEnemyToPool returnToPool = component.gameObject.AddComponent<ReturnEnemyToPool>();
        returnToPool.Setup(pool);

        return component;
    }
}