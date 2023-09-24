using UnityEngine;

public interface IProjectileInstantiator
{
    void CreateProjectile(Vector3 pos, Quaternion rot, Vector3 direction, float speed, float towerDamage);
}

public class ProjectileInstantiator : IProjectileInstantiator
{
    readonly ProjectilePool _pool;

    public ProjectileInstantiator(Projectile poolPrefab)
    {
        _pool = new ProjectilePool(poolPrefab);
    }

    public void CreateProjectile(Vector3 pos, Quaternion rot, Vector3 direction, float speed, float towerDamage)
    {
        Projectile poolMember = _pool.Pool.Get();
        poolMember.Launch(pos, rot, direction, speed, towerDamage);
    }
}
