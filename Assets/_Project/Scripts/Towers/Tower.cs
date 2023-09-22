using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    ITowerPresentation towerPresentation;

    protected int level;

    public void Upgrade()
    {
        level++;
    }

    public virtual void Setup()
    {
        towerPresentation = new TowerPresentation();
        towerPresentation.Show(transform.GetChild(0).gameObject);
    }
}
