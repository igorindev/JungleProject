using UnityEngine;

public class Projectile : PoolMember<Projectile>
{
    [SerializeField] new Rigidbody rigidbody;
    [SerializeField] float duration = 5;

    public void Launch(Vector3 pos, Quaternion rot, Vector3 direction, float speed)
    {
        rigidbody.velocity = Vector3.zero;
        gameObject.SetActive(true);
        rigidbody.position = pos;
        rigidbody.rotation = rot;
        rigidbody.AddForce(direction * speed);

        Invoke(nameof(Disable), duration);
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }

    void OnDisable()
    {
        Recicle();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out IHealth health))
        {
            health.ReceiveDamage(1);
        }

        Disable();
    }
}
