using System;
using UnityEngine;

public interface ITowerUpgrader
{
    void PickTower();
}

public class TowerUpgrader : ITowerUpgrader
{
    readonly IUIViewFactory _uiViewFactory;
    readonly IPlayerEconomy _playerEconomy;
    readonly IPlayerInput _playerInput;
    readonly UITowerUpgradeView _towerUpgradeView;
    IUIViewController _towerUpgraderViewController;
    LayerMask _towersMask;

    readonly Camera _cam;

    readonly Action _onCompleteUpgrade;

    public TowerUpgrader(
        IUIViewFactory uiViewFactory, 
        IPlayerEconomy playerEconomy, 
        IPlayerInput playerInput, 
        UITowerUpgradeView towerUpgradeView, 
        LayerMask towersMask,
        Action onCompleteUpgrade)
    {
        _cam = Camera.main;
        _playerInput = playerInput;
        _towerUpgradeView = towerUpgradeView;
        _towersMask = towersMask;
        _uiViewFactory = uiViewFactory;
        _playerEconomy = playerEconomy;
        _onCompleteUpgrade = onCompleteUpgrade;
    }

    public void PickTower()
    {
        if (HasUpgradableTowerAtPosition(out ITowerUpgradable tower) && !_playerInput.IsPointerOverUIObject())
        {
            SelectTower(tower);
        }
    }

    bool HasUpgradableTowerAtPosition(out ITowerUpgradable tower)
    {
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
        tower = null;
        if (Physics.Raycast(ray, out RaycastHit hit, 10000, _towersMask, QueryTriggerInteraction.Ignore))
        {
            tower = hit.collider.GetComponent<ITowerUpgradable>();
        }

        return tower != null;
    }

    void SelectTower(ITowerUpgradable tower)
    {
        Time.timeScale = 0;
        ClearView();
        _towerUpgraderViewController = _uiViewFactory.CreateTowerUpgradeViewController(_towerUpgradeView, tower, CanUpgradeTower, UpgradeTower);
    }

    bool CanUpgradeTower(ITowerUpgradable tower)
    {
        return tower.CanUpgrade() && tower.CanBuyUpgrade(_playerEconomy.GetNumOfCoins());
    }

    void UpgradeTower(ITowerUpgradable tower)
    {
        if (tower != null)
        {
            _playerEconomy.RemoveCoins(tower.GetUpgradeCost());
            tower.Upgrade();
        }
        else
        {
            Time.timeScale = 1;
            ClearView();
            _onCompleteUpgrade?.Invoke();
        }
    }

    void ClearView()
    {
        if (_towerUpgraderViewController != null)
        {
            _towerUpgraderViewController.Destroy();
            _towerUpgraderViewController = null;
        }
    }
}
