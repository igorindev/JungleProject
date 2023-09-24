using UnityEngine;

public interface IProjectile
{
    void Launch(Vector3 pos, Quaternion rot, Vector3 direction, float speed, float towerDamage);
    void Disable();
}

public class Projectile : MonoBehaviour, IProjectile
{
    [SerializeField] protected new Rigidbody rigidbody;
    [SerializeField] protected float duration = 5;

    protected float _speed;
    protected float _towerDamage;

    public virtual void Launch(Vector3 pos, Quaternion rot, Vector3 direction, float speed, float towerDamage)
    {
        rigidbody.velocity = Vector3.zero;
        transform.SetPositionAndRotation(pos, rot);
        gameObject.SetActive(true);
        rigidbody.AddForce(direction * speed);
        _towerDamage = towerDamage;
        _speed = speed;
        Invoke(nameof(Disable), duration);
    }

    public void Disable()
    {
        CancelInvoke(nameof(Disable));
        gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider collider)
    {
        HandleCollision(collider);
    }

    protected virtual void HandleCollision(Collider collider)
    {
        if (collider.TryGetComponent(out IHealth health))
        {
            health.ReceiveDamage(_towerDamage);
        }

        Disable();
    }
}
