using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public interface IUITowerUpgradeView
{
    void OnConfirmUpgrade(ITowerUpgradable data);
    void Setup(ITowerUpgradable interactedTower, Func<ITowerUpgradable, bool> _canUpgrade, Action<ITowerUpgradable> onConfirmUpgrade);
}

public class UITowerUpgradeView : UIView, IUITowerUpgradeView
{
    [Header("SELECTION INFO")]
    [SerializeField] TextMeshProUGUI _towerName;
    [SerializeField] TextMeshProUGUI _towerDamage;
    [SerializeField] TextMeshProUGUI _towerSpeed;
    [SerializeField] TextMeshProUGUI _towerCost;
    [SerializeField] TextMeshProUGUI _towerCostUpgrade;
    [SerializeField] TextMeshProUGUI _towerDescription;

    [Header("BUTTONS")]
    [SerializeField] Button _cancelButton;
    [SerializeField] Button _confirmButton;

    Action<ITowerUpgradable> _onConfirmUpgrade;
    Func<ITowerUpgradable, bool> _canUpgrade;

    public void Setup(ITowerUpgradable interactedTower, Func<ITowerUpgradable, bool> canUpgrade, Action<ITowerUpgradable> onConfirmUpgrade)
    {
        _canUpgrade = canUpgrade;
        _confirmButton.interactable = _canUpgrade.Invoke(interactedTower);

        TowerData data = interactedTower.GetTowerData();
        _onConfirmUpgrade = onConfirmUpgrade;
        UpdateContent(data, interactedTower.GetTowerCurrentLevel());

        _cancelButton.onClick.AddListener(() => OnConfirmUpgrade(null));
        _confirmButton.onClick.AddListener(() => OnConfirmUpgrade(interactedTower));
    }

    void UpdateContent(TowerData data, int currentLevel)
    {
        _towerName.text = "Name: " + data.TowerName;
        _towerDescription.text = data.TowerDescription;
        _towerDamage.text = "Damage: " + data.TowerDamage * currentLevel + " -> " + data.TowerDamage * (currentLevel + 1);
        _towerSpeed.text = "AtkSpeed: " + data.TowerSpeed * currentLevel + " -> " + data.TowerSpeed * (currentLevel + 1);
        _towerCost.text = "Buy Cost: " + data.TowerCost * currentLevel + " -> " + data.TowerCost * (currentLevel + 1);
        _towerCostUpgrade.text = "Upgrade Cost: " + data.TowerUpgradeCost * currentLevel + " -> " + data.TowerUpgradeCost * (currentLevel + 1);
    }

    public void OnConfirmUpgrade(ITowerUpgradable tower)
    {
        _onConfirmUpgrade?.Invoke(tower);

        if (tower != null)
            _confirmButton.interactable = (bool)(_canUpgrade?.Invoke(tower));
    }
}
