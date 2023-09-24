using UnityEngine;

public class EnemySpeed : Enemy
{
    [SerializeField] float _effectRange = 8;
    [SerializeField] float _speedMultiplier = 2;
    [SerializeField] LayerMask _layerMask;

    Collider[] targets;

    public override void Setup(EnemyData enemyData, int round)
    {
        base.Setup(enemyData, round);

        Invoke(nameof(AddSpeed), 2);
    }
    void AddSpeed()
    {
        targets = Physics.OverlapSphere(transform.position, _effectRange, _layerMask);
        foreach (Collider item in targets)
        {
            item.GetComponent<ISpeed>().AddSpeed(_speedMultiplier);
        }
    }

    void OnDisable()
    {
        foreach (Collider item in targets)
        {
            item.GetComponent<ISpeed>().RestoreSpeed();
        }
    }
}