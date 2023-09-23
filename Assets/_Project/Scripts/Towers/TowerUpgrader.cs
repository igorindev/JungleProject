using System;
using UnityEngine;

public interface ITowerUpgrader
{
    void PickTower();
    void UpgradeTower(ITowerUpgradable tower);
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
        if (_towerUpgraderViewController != null)
        {
            _towerUpgraderViewController.Destroy();
            _towerUpgraderViewController = null;
        }
        _towerUpgraderViewController = _uiViewFactory.CreateTowerUpgradeViewController(_towerUpgradeView, tower, CanUpgradeTower, UpgradeTower);
    }

    public bool CanUpgradeTower(ITowerUpgradable tower)
    {
        return tower.CanUpgrade() && tower.CanBuyUpgrade(_playerEconomy.GetNumOfCoins());
    }

    public void UpgradeTower(ITowerUpgradable tower)
    {
        if (tower != null)
        {
            _playerEconomy.RemoveCoins(tower.GetUpgradeCost());
            tower.Upgrade();
        }
        else
        {
            if (_towerUpgraderViewController != null)
            {
                _towerUpgraderViewController.Destroy();
                _towerUpgraderViewController = null;
            }
            Time.timeScale = 1;
            _onCompleteUpgrade?.Invoke();
        }
    }
}
