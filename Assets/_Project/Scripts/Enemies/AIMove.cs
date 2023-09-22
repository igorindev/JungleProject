using UnityEngine;
using UnityEngine.AI;

public interface IAIMove
{
    void Setup(NavMeshAgent navMeshAgent, float moveSpeed);
    void Move();
}

public class AIMove : IAIMove
{
    NavMeshAgent _agent;

    public void Setup(NavMeshAgent navMeshAgent, float moveSpeed)
    {
        _agent = navMeshAgent;
        _agent.speed = moveSpeed;

        Move();
    }

    public void Move()
    {
        _agent.SetDestination(Vector3.zero);
    }
}
