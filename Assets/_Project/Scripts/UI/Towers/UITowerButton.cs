using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public interface IUITowerButton
{
    void Setup(TowerData towerData, Action<TowerData> selectTower);
}

public class UITowerButton : MonoBehaviour, IUITowerButton
{
    [SerializeField] Image _towerSprite;
    [SerializeField] TextMeshProUGUI _towerName;
    [SerializeField] Button _button;

    public void Setup(TowerData towerData, Action<TowerData> selectTower)
    {
        _towerName.text = towerData.TowerName;
        _towerSprite.sprite = towerData.TowerSprite;
        _button.onClick.AddListener(() => { selectTower.Invoke(towerData); });
    }
}
