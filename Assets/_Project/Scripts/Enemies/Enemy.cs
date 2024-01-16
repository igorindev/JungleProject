using UnityEngine;
using UnityEngine.AI;

public interface IEnemy
{
    void Setup(EnemyData enemyData, int round);
}

public interface ISpeed
{
    void AddSpeed(float speedMultiplier);
    void RestoreSpeed();
}

public abstract class Enemy : MonoBehaviour, IEnemy, ISpeed
{
    protected EnemyData _enemyData;
    protected int _currentRound;

    IAIMove _aiMove;
    IHealth _health;

    public virtual void Setup(EnemyData enemyData, int round)
    {
        _enemyData = enemyData;
        _currentRound = round;

        _health = GetComponent<IHealth>();
        _health.Setup(enemyData.EnemyHealth + ((round - 1) * 0.25f));
        _aiMove = new AIMove();
        _aiMove.Setup(GetComponent<NavMeshAgent>(), enemyData.EnemySpeed);
    }

    public void AddSpeed(float speed)
    {
        _aiMove.AddSpeed(speed);
    }

    public void RestoreSpeed()
    {
        _aiMove.RestoreSpeed();
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
