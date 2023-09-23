using UnityEngine;

public class TowerCommon : Tower, IAttack
{
    [SerializeField] float shotSpeed = 1000;
    [SerializeField] LayerMask layerMask;

    float count;
    float radius = 100;

    IProjectileInstantiator projectileInstantiator;

    [SerializeField] Projectile prefab; 

    public float Damage { get; set; } = 1;
    public float TowerSpeed { get => _towerData.TowerSpeed / _level; }
    public float TowerDamage { get => _towerData.TowerDamage * _level; }

    readonly Collider[] colliders = new Collider[25];

    void Awake()
    {
        enabled = false;
    }

    public override void Setup(TowerData towerData)
    {
        enabled = true;
        base.Setup(towerData);

        projectileInstantiator = new ProjectileInstantiator(prefab);
    }

    public void ApplyDamage()
    {
        
    }

    void Update()
    {
        if (count > TowerSpeed)
        {
            Transform target = GetNextTarget();
            if (target)
            {
                Vector3 pos = transform.position;
                pos.y = target.position.y;
                Vector3 dir = (target.position - pos).normalized;

                projectileInstantiator.CreateProjectile(transform.position + Vector3.up, Quaternion.identity, dir, shotSpeed, TowerDamage);
            }

            count -= TowerSpeed;
        }

        count += Time.deltaTime;
    }

    Transform GetNextTarget()
    {
        int colliderHits = Physics.OverlapSphereNonAlloc(transform.position, radius, colliders, layerMask);
        if (colliderHits > 0)
        {
            return colliders[Random.Range(0, colliderHits)].transform;
        }

        return null;
    }
}
