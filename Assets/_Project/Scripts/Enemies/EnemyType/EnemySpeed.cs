using UnityEngine;

public class EnemySpeed : Enemy
{
    [SerializeField] float _effectRange = 8;
    [SerializeField] float _speedMultiplier = 2;
    [SerializeField] LayerMask _layerMask;
    [SerializeField] Transform _range;

    Collider[] targets;

    public override void Setup(EnemyData enemyData, int round)
    {
        _range.gameObject.SetActive(true);
        base.Setup(enemyData, round);
        _range.localScale = _effectRange * 2 * Vector3.one;
        Invoke(nameof(AddSpeed), 2);
    }
    void AddSpeed()
    {
        _range.gameObject.SetActive(false);
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
            if (item && item.gameObject.activeSelf)
                item.GetComponent<ISpeed>().RestoreSpeed();
        }
    }
}