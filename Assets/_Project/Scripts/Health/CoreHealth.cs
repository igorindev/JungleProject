using UnityEngine;

public class CoreHealth : Health
{
    public override void ReceiveDamage(float amount)
    {
        base.ReceiveDamage(amount);
        transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, _currentHealth / _maxHealth);
    }
}
