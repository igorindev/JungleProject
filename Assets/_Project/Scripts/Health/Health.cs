using System;
using UnityEngine;

public interface IHealth
{
    void Initialize(float enemyHealth);
    void ReceiveDamage(float amount);
    Action OnDie { get; set; }
    Action OnDestroyGO { get; set; }
}

public class Health : MonoBehaviour, IHealth
{
    float _currentHealth;

    IHealthBar _healthBar;

    public Action OnDie { get; set; }
    public Action OnDestroyGO { get; set; }

    public void Initialize(float hp)
    {
        _healthBar = GetComponent<IHealthBar>();
        _currentHealth = hp;
    }

    public void ReceiveDamage(float amount)
    {
        _currentHealth -= amount;

        if (_currentHealth <= 0)
        {
            Die();
        }

        //_healthBar.SetCurrentHealth(_currentHealth);
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
