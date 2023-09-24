using UnityEngine;

public class TowerIce : Tower
{
    public override void Setup(TowerData towerData)
    {
        base.Setup(towerData);
    }

    void OnTriggerEnter(Collider other)
    {
        //if (other.TryGetComponent(out ISpeed speed))
        //{
        //    speed.ReduceSpeed();
        //}
    }
}
