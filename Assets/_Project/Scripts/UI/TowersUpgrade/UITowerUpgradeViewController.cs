using System;

public class UITowerUpgradeViewController : UIViewController<UITowerUpgradeView>
{
    readonly int _collectionSize;
    readonly Func<int, TowerData> _getTowerData;

    readonly Action<TowerData, Action> _selectTower;

    public UITowerUpgradeViewController(UITowerUpgradeView view, int collectionSize, Func<int, TowerData> getTowerData, Action<TowerData, Action> selectTower) : base(view)
    {
        _collectionSize = collectionSize;
        _getTowerData = getTowerData;
        _selectTower = selectTower;
    }

    public override void Present()
    {
        _view.Setup(_collectionSize, _getTowerData, OnSelectTower);

        base.Present();
    }

    void OnSelectTower(TowerData data, Action action)
    {
        _selectTower?.Invoke(data, action);
    }
}
