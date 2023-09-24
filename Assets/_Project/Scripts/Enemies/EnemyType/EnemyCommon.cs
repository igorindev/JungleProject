using UnityEngine.AI;

public class EnemyCommon : Enemy
{
    IAIMove _aiMove;
    IHealth _health;

    public override void Setup(EnemyData enemyData, int round)
    {
        _health = GetComponent<IHealth>();
        _health.Initialize(enemyData.EnemyHealth * round);
        _aiMove = new AIMove();
        _aiMove.Setup(GetComponent<NavMeshAgent>(), enemyData.EnemySpeed);

        base.Setup(enemyData, round);
    }
}
