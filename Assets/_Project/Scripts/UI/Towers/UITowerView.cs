using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public interface IUITowerView
{
    void Setup(int collectionSize, Func<int, TowerData> getTowerData, Action<TowerData, Action> onSelectTower);
}

public class UITowerView : UIView, IUITowerView
{
    [Header("SELECTION INFO")]
    [SerializeField] GameObject _contentSelection;
    [SerializeField] TextMeshProUGUI _towerName;
    [SerializeField] TextMeshProUGUI _towerDamage;
    [SerializeField] TextMeshProUGUI _towerSpeed;
    [SerializeField] TextMeshProUGUI _towerCost;
    [SerializeField] TextMeshProUGUI _towerCostUpgrade;
    [SerializeField] TextMeshProUGUI _towerDescription;

    [Header("CONTENT")]
    [SerializeField] RectTransform _contentTransform;

    [Header("BUTTONS")]
    [SerializeField] Button _cancelSelectionButton;
    [SerializeField] UITowerButton _prefabTowerButton;

    readonly List<UITowerButton> _towerButtons = new List<UITowerButton>();

    public void Setup(int collectionSize, Func<int, TowerData> getTowerData, Action<TowerData, Action> onSelectTower)
    {
        for (int i = 0; i < collectionSize; i++)
        {
            TowerData data = getTowerData.Invoke(i);
            UITowerButton button = Instantiate(_prefabTowerButton, _contentTransform);

            button.Setup(data, SelectTower);

            void SelectTower(TowerData data)
            {
                _contentTransform.gameObject.SetActive(false);
                _cancelSelectionButton.gameObject.SetActive(true);
                _contentSelection.SetActive(true);
                onSelectTower?.Invoke(data, DeselectTower);
                UpdateContent(data);
            }

            _towerButtons.Add(button);
        }

        _cancelSelectionButton.onClick.AddListener(DeselectTower);

        void DeselectTower()
        {
            _contentTransform.gameObject.SetActive(true);
            _cancelSelectionButton.gameObject.SetActive(false);
            onSelectTower?.Invoke(null, null);
            _contentSelection.SetActive(false);
        }
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
}
