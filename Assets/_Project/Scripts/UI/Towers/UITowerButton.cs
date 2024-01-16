using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public interface IUITowerButton
{
    void Setup(ITowerData towerData, Action<ITowerData> selectTower);
}

public class UITowerButton : MonoBehaviour, IUITowerButton
{
    [SerializeField] TextMeshProUGUI _towerName;
    [SerializeField] TextMeshProUGUI _towerCost;
    [SerializeField] Button _button;

    public void Setup(ITowerData towerData, Action<ITowerData> selectTower)
    {
        _towerName.text = towerData.TowerName;
        _towerCost.text = "$" + towerData.TowerCost;
        _button.onClick.AddListener(() => { selectTower.Invoke(towerData); });
    }
}
