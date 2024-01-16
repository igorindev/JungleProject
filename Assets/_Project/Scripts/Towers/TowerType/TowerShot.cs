using UnityEngine;

public class TowerShot : Tower
{
    [SerializeField] float _shotSpeed = 1000;
    [SerializeField] LayerMask _layerMask;
    [SerializeField] Transform _radiusObject;

    float _count;

    IProjectileInstantiator projectileInstantiator;
    ITowerRadiusPresentation towerRadiusArea;

    [SerializeField] Projectile prefab; 

    public float Damage { get; set; } = 1;
    public float TowerAtkSpeed { get => _towerData.TowerSpeed / _level; }
    public float TowerDamage { get => _towerData.TowerDamage; }
    public float TowerRadius { get => _towerData.TowerRadius * _level; }

    readonly Collider[] _colliders = new Collider[25];

    void Awake()
    {
        enabled = false;
    }

    public override void Setup(ITowerData towerData)
    {
        enabled = true;
        base.Setup(towerData);

        projectileInstantiator = new ProjectileInstantiator(prefab);
        towerRadiusArea = new TowerRadiusPresentation(TowerRadius, _radiusObject);
    }

    void Update()
    {
        CalculateProjectileLaunch();
    }

    void CalculateProjectileLaunch()
    {
        if (_count > TowerAtkSpeed)
        {
            Transform target = GetNextTarget();
            if (target)
            {
                Vector3 pos = transform.position;
                pos.y = target.position.y;
                Vector3 dir = (target.position - pos).normalized;

                projectileInstantiator.CreateProjectile(transform.position + Vector3.up, Quaternion.identity, dir, _shotSpeed, TowerDamage);
            }

            _count -= TowerAtkSpeed;
        }

        _count += Time.deltaTime;
    }

    Transform GetNextTarget()
    {
        int colliderHits = Physics.OverlapSphereNonAlloc(transform.position, TowerRadius, _colliders, _layerMask);
        if (colliderHits > 0)
        {
            return _colliders[Random.Range(0, colliderHits)].transform;
        }

        return null;
    }

    public override void Upgrade()
    {
        base.Upgrade();
        towerRadiusArea.UpdateRadius(TowerRadius);
    }
}
