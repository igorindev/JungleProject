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
    [SerializeField] TextMeshProUGUI _towerRadius;
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

        ITowerData data = interactedTower.GetTowerData();
        _onConfirmUpgrade = onConfirmUpgrade;
        UpdateContent(data, interactedTower.GetTowerCurrentLevel());

        _cancelButton.onClick.AddListener(() => OnConfirmUpgrade(null));
        _confirmButton.onClick.AddListener(() => OnConfirmUpgrade(interactedTower));
    }

    void UpdateContent(ITowerData data, int currentLevel)
    {
        _towerName.text = "Name: " + data.TowerName;
        _towerDescription.text = data.TowerDescription;
        _towerDamage.text = "Damage: " + data.TowerDamage;
        _towerSpeed.text = "AtkSpeed: " + data.TowerSpeed * currentLevel + " -> " + data.TowerSpeed * (currentLevel + 1);
        _towerRadius.text = "Range: " + data.TowerRadius * currentLevel + " -> " + data.TowerRadius * (currentLevel + 1);
        _towerCostUpgrade.text = "Upgrade: " + data.TowerUpgradeCost * currentLevel + " -> " + data.TowerUpgradeCost * (currentLevel + 1);
    }

    public void OnConfirmUpgrade(ITowerUpgradable tower)
    {
        _onConfirmUpgrade?.Invoke(tower);

        if (tower != null)
        {
            _confirmButton.interactable = (bool)(_canUpgrade?.Invoke(tower));
            UpdateContent(tower.GetTowerData(), tower.GetTowerCurrentLevel());
        }
    }
}
