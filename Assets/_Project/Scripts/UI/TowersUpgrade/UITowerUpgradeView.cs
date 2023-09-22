using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public interface IUITowerUpgradeView
{
    void Setup(Tower interactedTower, Action<TowerData> onConfirmUpgrade);
}

public class UITowerUpgradeView : UIView, IUITowerUpgradeView
{
    [Header("SELECTION INFO")]
    [SerializeField] GameObject _contentSelection;
    [SerializeField] TextMeshProUGUI _towerName;
    [SerializeField] TextMeshProUGUI _towerDamage;
    [SerializeField] TextMeshProUGUI _towerSpeed;
    [SerializeField] TextMeshProUGUI _towerCost;
    [SerializeField] TextMeshProUGUI _towerCostUpgrade;
    [SerializeField] TextMeshProUGUI _towerDescription;

    [Header("BUTTONS")]
    [SerializeField] Button _cancelSelectionButton;

    Action<TowerData> _onConfirmUpgrade;

    public void Setup(Tower interactedTower, Action<TowerData> onConfirmUpgrade)
    {
        TowerData data = interactedTower.TowerData;
        _onConfirmUpgrade = onConfirmUpgrade;
        UpdateContent(data);
    }

    void UpdateContent(TowerData data)
    {
        _towerName.text = "Name: " + data.TowerName;
        _towerDamage.text = "Damage: " + data.TowerDamage;
        _towerSpeed.text = "AtkSpeed: " + data.TowerSpeed;
        _towerCost.text = "Buy Cost: " + data.TowerCost;
        _towerCostUpgrade.text = "Upgrade Cost: " + data.TowerUpgradeCost;
        _towerDescription.text = data.TowerDescription;
    }

    public void OnConfirmUpgrade(TowerData data)
    {
        _onConfirmUpgrade?.Invoke(data);
    }
}
