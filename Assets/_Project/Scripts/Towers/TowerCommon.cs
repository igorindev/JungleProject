using UnityEngine;

public class TowerCommon : Tower, IAttack
{
    [SerializeField] float shotSpeed = 1000;
    [SerializeField] float shootBaseDelay = 2;
    [SerializeField] LayerMask layerMask;

    float shootDelay = 2;
    float count;
    float radius = 100;

    ProjectileInstantiator projectileInstantiator;

    [SerializeField] Projectile prefab; 

    public float Damage { get; set; } = 1;

    Collider[] colliders = new Collider[1];

    private void Start()
    {
        shootDelay = shootBaseDelay;
        projectileInstantiator = new ProjectileInstantiator(prefab);
    }

    public void ApplyDamage()
    {
        
    }

    void Update()
    {
        if (count > shootDelay)
        {
            Transform target = GetNextTarget();
            if (target)
            {
                Vector3 pos = transform.position;
                pos.y = target.position.y;
                Vector3 dir = (target.position - pos).normalized;

                projectileInstantiator.CreateProjectile(transform.position + Vector3.up, Quaternion.identity, dir, shotSpeed);
                //shoot
            }

            count -= shootBaseDelay;
        }

        count += Time.deltaTime;
    }

    Transform GetNextTarget()
    {
        if (Physics.OverlapSphereNonAlloc(transform.position, radius, colliders, layerMask) > 0)
        {
            return colliders[0].transform;
        }

        return null;
    }
}
