using UnityEngine;

public interface ITower
{
    void Setup(ITowerData towerData);
    ITowerData GetTowerData();
    int GetTowerCurrentLevel();
}

public interface ITowerUpgradable : ITower
{
    bool CanBuyUpgrade(int contain);
    void Upgrade();
    int GetUpgradeCost();
    bool CanUpgrade();
}

public interface ITowerSelect
{
    void SaveOriginalMaterial();
    Material GetOriginalMaterial();
}

public abstract class Tower : MonoBehaviour, ITowerUpgradable, ITowerSelect
{
    ITowerPresentation _towerPresentation;

    protected int _level = 1;
    protected const int _maxLevel = 3;

    protected ITowerData _towerData;

    Material _originalMaterial;

    public virtual void Setup(ITowerData towerData)
    {
        _towerData = towerData;
        _towerPresentation = new TowerPresentation();
        _towerPresentation.Show(transform.GetChild(0).gameObject);

        SaveOriginalMaterial();
    }

    public bool CanBuyUpgrade(int coins)
    {
        return coins >= _towerData.TowerUpgradeCost * _level;
    }

    public virtual void Upgrade()
    {
        _level++;
    }

    public bool CanUpgrade()
    {
        return _level < _maxLevel;
    }

    public int GetUpgradeCost()
    {
        return _towerData.TowerUpgradeCost * _level;
    }

    public ITowerData GetTowerData()
    {
        return _towerData;
    }

    public int GetTowerCurrentLevel()
    {
        return _level;
    }

    public void SaveOriginalMaterial()
    {
        _originalMaterial = GetComponentInChildren<MeshRenderer>().sharedMaterial;
    }

    public Material GetOriginalMaterial()
    {
        return _originalMaterial;
    }
}