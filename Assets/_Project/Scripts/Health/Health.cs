using System;
using UnityEngine;

public interface IHealth
{
    void Initialize();
    void ReceiveDamage(float amount);
    Action OnDie { get; set; }
}

public class Health : MonoBehaviour, IHealth
{
    [SerializeField] float _maxHealth;
    float _currentHealth;

    IHealthBar _healthBar;

    public Action OnDie { get; set; }

    public void Initialize()
    {
        _healthBar = GetComponent<IHealthBar>();
        _currentHealth = _maxHealth;
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
        Destroy(gameObject);
    }
}
