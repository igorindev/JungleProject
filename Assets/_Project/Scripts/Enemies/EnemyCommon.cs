using System;
using UnityEngine.AI;

public class EnemyCommon : Enemy
{
    IAIMove _aiMove;
    IHealth _health;

    public override void Setup(EnemyData enemyData, Action onEnemyKilled)
    {
        _health = GetComponent<IHealth>();
        _health.Initialize();
        _aiMove = new AIMove();
        _aiMove.Setup(GetComponent<NavMeshAgent>(), enemyData.EnemySpeed);

        base.Setup(enemyData, onEnemyKilled);
    }
}
