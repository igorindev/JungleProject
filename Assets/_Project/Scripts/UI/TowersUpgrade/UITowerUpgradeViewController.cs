using System;

public class UITowerUpgradeViewController : UIViewController<UITowerUpgradeView>
{
    private readonly IUIViewController viewController;
    private Tower _interactedTower;
    private Action<TowerData> _onConfirmUpgrade;

    public UITowerUpgradeViewController(UITowerUpgradeView view, IUIViewController viewController, Tower interactedTower, Action<TowerData> onConfirmUpgrade) : base(view)
    {
        this.viewController = viewController;
        _interactedTower = interactedTower;
        _onConfirmUpgrade = onConfirmUpgrade;
    }

    public override void Present()
    {
        _view.Setup(_interactedTower, OnConfirmUpgrade);

        viewController.Hide();

        base.Present();
    }

    void OnConfirmUpgrade(TowerData data)
    {
        _onConfirmUpgrade?.Invoke(data);
        viewController.Present();
    }
}
