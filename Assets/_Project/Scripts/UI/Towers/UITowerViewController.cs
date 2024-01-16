using System;

public class UITowerViewController : UIViewController<UITowerView>
{
    readonly int _collectionSize;
    readonly Func<int, ITowerData> _getTowerData;

    readonly Action<ITowerData> _selectTower;

    public UITowerViewController(UITowerView view, int collectionSize, Func<int, ITowerData> getTowerData, Action<ITowerData> selectTower) : base(view)
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

    void OnSelectTower(ITowerData data)
    {
        _selectTower?.Invoke(data);
    }
}
