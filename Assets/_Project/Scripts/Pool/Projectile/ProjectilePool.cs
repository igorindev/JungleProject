using UnityEngine;

public class ProjectilePool : PoolBase<Projectile>
{
    public ProjectilePool(Projectile prefab) : base(prefab) { }

    protected override Projectile CreatePooledItem()
    {
        Projectile component = Object.Instantiate(_prefab);

        // This is used to return ParticleSystems to the pool when they have stopped.
        ReturnProjectileToPool returnToPool = component.gameObject.AddComponent<ReturnProjectileToPool>();
        returnToPool.Setup(pool);

        return component;
    }
}