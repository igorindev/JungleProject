using UnityEngine;

public interface IEnemy
{
    void Setup(EnemyData enemyData);
}

public abstract class Enemy : MonoBehaviour, IEnemy
{
    protected EnemyData _enemyData;

    public virtual void Setup(EnemyData enemyData)
    {
        _enemyData = enemyData;
    }
}
