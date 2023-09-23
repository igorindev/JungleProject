using UnityEngine;

public class Projectile : PoolMember<Projectile>
{
    [SerializeField] new Rigidbody rigidbody;
    [SerializeField] float duration = 5;

    float _towerDamage;

    public void Launch(Vector3 pos, Quaternion rot, Vector3 direction, float speed, float towerDamage)
    {
        rigidbody.velocity = Vector3.zero;
        transform.SetPositionAndRotation(pos, rot);
        gameObject.SetActive(true);
        rigidbody.AddForce(direction * speed);
        _towerDamage = towerDamage;
        Invoke(nameof(Disable), duration);
    }

    void Disable()
    {
        CancelInvoke(nameof(Disable));
        gameObject.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out IHealth health))
        {
            health.ReceiveDamage(_towerDamage);
        }

        Disable();
    }
}
