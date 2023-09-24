using UnityEngine;

public class TowerBomb : Tower
{
    [SerializeField] LayerMask _layerMask;
    [SerializeField] Transform _radiusObject;
    [SerializeField] SphereCollider _rangeCollider;

    float _count;

    ITowerRadiusPresentation towerRadiusArea;

    public float TowerRadius { get => _towerData.TowerRadius * _level; }

    bool initializedExplosion;

    public override void Setup(TowerData towerData)
    {
        base.Setup(towerData);

        towerRadiusArea = new TowerRadiusPresentation(TowerRadius, _radiusObject);
        _rangeCollider.radius = TowerRadius;
    }

    void Update()
    {
        if (initializedExplosion)
        {
            if (_count > 2f)
            {
                Explode();
            }

            _count += Time.deltaTime;
        }
    }

    Collider[] GetTargets()
    {
        return Physics.OverlapSphere(transform.position, TowerRadius, _layerMask);
    }

    void Explode()
    {
        Collider[] targets = GetTargets();
        foreach (var item in targets)
        {
            item.GetComponent<IHealth>().ReceiveDamage(_towerData.TowerDamage);
        }
        Destroy(gameObject);
    }

    public override void Upgrade()
    {
        base.Upgrade();
        towerRadiusArea.UpdateRadius(TowerRadius);
        _rangeCollider.radius = TowerRadius;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
            initializedExplosion = true;
    }
}
