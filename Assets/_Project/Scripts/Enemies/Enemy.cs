using System;
using UnityEngine;

public interface IAttack
{
    float Damage { get; set; }
    void ApplyDamage();
}

public abstract class Enemy : MonoBehaviour
{
    Action _onEnemyKilled;

    public virtual void Setup(EnemyData enemyData, Action onEnemyKilled)
    {
        _onEnemyKilled = onEnemyKilled;
    }

    void OnDestroy()
    {
        _onEnemyKilled?.Invoke();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CoreHealth coreHealth))
        {
            coreHealth.ReceiveDamage(1);
            Destroy(gameObject);
        }
    }
}
