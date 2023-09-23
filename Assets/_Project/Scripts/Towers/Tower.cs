using System;
using UnityEngine;

public interface ITower
{
    void Setup(TowerData towerData);
    TowerData GetTowerData();
    int GetTowerCurrentLevel();
}

public interface ITowerUpgradable : ITower
{
    bool CanBuyUpgrade(int contain);
    void Upgrade();
    int GetUpgradeCost();
    bool CanUpgrade();
}

public abstract class Tower : MonoBehaviour, ITowerUpgradable
{
    ITowerPresentation _towerPresentation;

    protected int _level = 1;
    const int _maxLevel = 3;

    TowerData _towerData;

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

    public TowerData GetTowerData()
    {
        return _towerData;
    }

    public int GetTowerCurrentLevel()
    {
        return _level;
    }
}