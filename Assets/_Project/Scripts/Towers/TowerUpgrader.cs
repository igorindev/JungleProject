using UnityEngine;

public interface ITowerUpgrader
{
    void Setup(IUIViewFactory uiViewFactory, IPlayerEconomy playerEconomy, IUIViewControllerAccess towerView);
    void UpgradeTower(IUpgradable tower);
}

public class TowerUpgrader : MonoBehaviour, ITowerUpgrader
{
    IUIViewFactory _uiViewFactory;
    IPlayerEconomy _playerEconomy;
    IUIViewController _towerViewController;
    IUIViewController _towerUpgraderViewController;

    public void Set()
    {
        _towerViewController.Hide();
        _towerUpgraderViewController.Show();
    }

    public void Setup(IUIViewFactory uiViewFactory, IPlayerEconomy playerEconomy, IUIViewControllerAccess towerView)
    {
        _uiViewFactory = uiViewFactory;
        _playerEconomy = playerEconomy;
        _towerViewController = towerView.GetViewController();

        //_towerUpgraderViewController = _uiViewFactory.CreateTowerUpgradeViewController();
        //_towerUpgraderViewController.Hide();
    }

    public void UpgradeTower(IUpgradable tower)
    {
        if (tower.CanUpgrade() && tower.CanBuyUpgrade(_playerEconomy.GetNumOfCoins()))
        {
            _playerEconomy.RemoveCoins(tower.GetUpgradeCost());
            tower.Upgrade();
        }
    }
}
