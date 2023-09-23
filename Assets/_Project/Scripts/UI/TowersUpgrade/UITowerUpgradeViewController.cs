using System;

public class UITowerUpgradeViewController : UIViewController<UITowerUpgradeView>
{
    private readonly ITowerUpgradable _interactedTower;
    private readonly Action<ITowerUpgradable> _onConfirmUpgrade;

    readonly Func<ITowerUpgradable, bool> _canUpgrade;

    public UITowerUpgradeViewController(UITowerUpgradeView view, ITowerUpgradable interactedTower, Func<ITowerUpgradable, bool> canUpgrade, Action<ITowerUpgradable> onConfirmUpgrade) : base(view)
    {
        _interactedTower = interactedTower;
        _canUpgrade = canUpgrade;
        _onConfirmUpgrade = onConfirmUpgrade;
    }

    public override void Present()
    {
        _view.Setup(_interactedTower, _canUpgrade, OnConfirmUpgrade);
    }

    void OnConfirmUpgrade(ITowerUpgradable data)
    {
        _onConfirmUpgrade?.Invoke(data);
    }
}
