using System;

public class UITowerViewController : UIViewController<UITowerView>
{
    readonly int _collectionSize;
    readonly Func<int, TowerData> _getTowerData;

    readonly Action<TowerData> _selectTower;

    public UITowerViewController(UITowerView view, int collectionSize, Func<int, TowerData> getTowerData, Action<TowerData> selectTower) : base(view)
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

    void OnSelectTower(TowerData data)
    {
        _selectTower?.Invoke(data);
    }
}
