using UnityEngine;

public class CoreHealth : Health
{
    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            ReceiveDamage(1);
            enemy.gameObject.SetActive(false);
        }
    }
}
