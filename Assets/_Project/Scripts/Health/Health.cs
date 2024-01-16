using System;
using UnityEngine;

public interface IHealth
{
    void Setup(float enemyHealth);
    void ReceiveDamage(float amount);
    Action OnDie { get; set; }
    Action OnDestroyGO { get; set; }
}

public class Health : MonoBehaviour, IHealth
{
    protected float _currentHealth;
    protected float _maxHealth;
    public Action OnDie { get; set; }
    public Action OnDestroyGO { get; set; }

    public void Setup(float hp)
    {
        _maxHealth = hp;
        _currentHealth = hp;
    }

    public virtual void ReceiveDamage(float amount)
    {
        _currentHealth -= amount;

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        OnDie?.Invoke();
        gameObject.SetActive(false);
    }

    void OnDisable()
    {
        OnDestroyGO?.Invoke();
    }
}
