using System;
using UnityEngine;

public interface ITower
{
    void Setup(TowerData towerData);
}

public interface IUpgradable
{
    bool CanBuyUpgrade(int contain);
    void Upgrade();
    int GetUpgradeCost();
    bool CanUpgrade();
}

public abstract class Tower : MonoBehaviour, ITower, IUpgradable
{
    ITowerPresentation _towerPresentation;

    protected int _level;
    const int _maxLevel = 3;

    TowerData _towerData;

    public TowerData TowerData { get => _towerData; }

    public virtual void Setup(TowerData towerData)
    {
        _towerData = towerData;
        _towerPresentation = new TowerPresentation();
        _towerPresentation.Show(transform.GetChild(0).gameObject);
    }

    public bool CanBuyUpgrade(int coins)
    {
        return coins >= _towerData.TowerUpgradeCost;
    }

    public void Upgrade()
    {
        _level++;
    }

    public bool CanUpgrade()
    {
        return _level < _maxLevel;
    }

    public int GetUpgradeCost()
    {
        return _towerData.TowerUpgradeCost;
    }
}