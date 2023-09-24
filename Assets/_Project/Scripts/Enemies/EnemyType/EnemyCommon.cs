using UnityEngine.AI;

public class EnemyCommon : Enemy
{
    IAIMove _aiMove;
    IHealth _health;

    public override void Setup(EnemyData enemyData)
    {
        _health = GetComponent<IHealth>();
        _health.Initialize(enemyData.EnemyHealth);
        _aiMove = new AIMove();
        _aiMove.Setup(GetComponent<NavMeshAgent>(), enemyData.EnemySpeed);

        base.Setup(enemyData);
    }
}
