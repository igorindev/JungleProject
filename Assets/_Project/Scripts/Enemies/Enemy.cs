using UnityEngine;

public interface IEnemy
{
    void Setup(EnemyData enemyData);
}

public abstract class Enemy : MonoBehaviour, IEnemy, IAttack
{
    protected EnemyData _enemyData;

    public virtual void Setup(EnemyData enemyData)
    {
        _enemyData = enemyData;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IHealth target))
        {
            target.ReceiveDamage(_enemyData.EnemyDamage);
            gameObject.SetActive(false);
        }
    }
}
