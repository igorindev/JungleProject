using UnityEngine;

public interface IEnemy
{
    void Setup(EnemyData enemyData, int round);
}

public abstract class Enemy : MonoBehaviour, IEnemy
{
    protected EnemyData _enemyData;
    protected int _currentRound;

    public virtual void Setup(EnemyData enemyData, int round)
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
