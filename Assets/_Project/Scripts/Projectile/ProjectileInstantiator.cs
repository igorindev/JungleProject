using UnityEngine;

public interface IProjectileInstantiator
{
    void CreateProjectile(Vector3 pos, Quaternion rot, Vector3 direction, float speed, float towerDamage);
}

public class ProjectileInstantiator : IProjectileInstantiator
{
    readonly Pool<Projectile> shoots;

    public ProjectileInstantiator(Projectile poolPrefab)
    {
        shoots = new Pool<Projectile>(poolPrefab);
    }

    public void CreateProjectile(Vector3 pos, Quaternion rot, Vector3 direction, float speed, float towerDamage)
    {
        Projectile poolMember = shoots.GetFromPool();
        poolMember.Launch(pos, rot, direction, speed, towerDamage);
    }
}
