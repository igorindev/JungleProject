using UnityEngine;

public class ProjectileInstantiator
{
    readonly Pool<Projectile> shoots;

    public ProjectileInstantiator(Projectile poolPrefab)
    {
        shoots = new Pool<Projectile>(poolPrefab);
    }

    public void CreateProjectile(Vector3 pos, Quaternion rot, Vector3 direction, float speed)
    {
        Projectile poolMember = shoots.GetFromPool();
        poolMember.Launch(pos, rot, direction, speed);
    }
}
