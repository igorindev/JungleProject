using UnityEngine;
using UnityEngine.AI;

public interface IAIMove : ISpeed
{
    void Setup(NavMeshAgent navMeshAgent, float moveSpeed);
    void Move();
}

public class AIMove : IAIMove
{
    NavMeshAgent _agent;
    float _baseSpeed;

    public void Setup(NavMeshAgent navMeshAgent, float moveSpeed)
    {
        _agent = navMeshAgent;
        _agent.speed = moveSpeed;
        _baseSpeed = moveSpeed;

        Move();
    }

    public void Move()
    {
        _agent.SetDestination(Vector3.zero);
    }

    public void RestoreSpeed()
    {
        _agent.speed = _baseSpeed;
    }

    public void AddSpeed(float speedMultiplier)
    {
        _agent.speed = _baseSpeed * speedMultiplier;
    }
}
