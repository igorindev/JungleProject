using UnityEngine;

public class CoreHealth : Health
{
    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            ReceiveDamage(1);
            Destroy(enemy.gameObject);
        }
    }
}
