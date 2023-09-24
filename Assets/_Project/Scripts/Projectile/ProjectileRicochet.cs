using System.Linq;
using UnityEngine;

public class ProjectileRicochet : Projectile
{
    [SerializeField] int _maxRicochetCount = 3;
    [SerializeField] int _searchRadius = 10;
    [SerializeField] LayerMask _targetMask;
    int _ricochetCount;

    Transform _lastTarget;

    readonly Collider[] _colliders = new Collider[10];

    protected override void HandleCollision(Collider collider)
    {
        _lastTarget = collider.transform;
        if (collider.TryGetComponent(out IHealth health))
        {
            health.ReceiveDamage(_towerDamage);
        }

        if (_ricochetCount == _maxRicochetCount)
        {
            ResetRicochet();
            return;
        }

        Transform target = GetNextTarget();
        if (target)
        {
            rigidbody.velocity = Vector3.zero;
            Vector3 t = target.position;
            t.y = transform.position.y;
            Vector3 dir = (t - transform.position).normalized;
            CancelInvoke(nameof(Disable));
            Launch(transform.position, Quaternion.identity, dir, _speed, _towerDamage * 0.5f);
            _ricochetCount++;
        }
        else
        {
            ResetRicochet();
        }
    }

    void ResetRicochet()
    {
        _ricochetCount = 0;
        Disable();
    }

    Transform GetNextTarget()
    {
        int colliderHits = Physics.OverlapSphereNonAlloc(transform.position, _searchRadius, _colliders, _targetMask);
        if (colliderHits > 0)
        {
            Transform target = _colliders.FirstOrDefault(x => x != null && x != _lastTarget).transform;
            return target;
        }

        return null;
    }
}
