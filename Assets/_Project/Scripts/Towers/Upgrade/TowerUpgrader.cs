using System;
using UnityEngine;

public interface ITowerUpgrader
{
    void UpdateSelectTower();
    bool PickTower();
}

public class TowerUpgrader : ITowerUpgrader
{
    readonly IUIViewFactory _uiViewFactory;
    readonly IPlayerEconomy _playerEconomy;
    readonly IPlayerInput _playerInput;
    readonly IUITowerUpgradeView _towerUpgradeView;
    readonly ITowerUpgraderSelectPresentation _towerUpgraderSelectPresentation;
    IUIViewController _towerUpgraderViewController;
    LayerMask _towersMask;

    readonly Camera _cam;

    readonly Action _onCompleteUpgrade;

    public TowerUpgrader(
        IUIViewFactory uiViewFactory, 
        IPlayerEconomy playerEconomy, 
        IPlayerInput playerInput,
        Material selectMaterial,
        IUITowerUpgradeView towerUpgradeView, 
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
        _towerUpgraderSelectPresentation = new TowerUpgraderSelectPresentation(_cam, towersMask, selectMaterial);
    }

    public bool PickTower()
    {
        if (HasUpgradableTowerAtPosition(out ITowerUpgradable tower) && !_playerInput.IsPointerOverUIObject())
        {
            PickTower(tower);
            return true;
        }

        return false;
    }

    public void UpdateSelectTower()
    {
        _towerUpgraderSelectPresentation.UpdateSelection();
    }

    void PickTower(ITowerUpgradable tower)
    {
        _towerUpgraderSelectPresentation.SetSelected();
        Time.timeScale = 0;
        ClearView();
        _towerUpgraderViewController = _uiViewFactory.CreateTowerUpgradeViewController(_towerUpgradeView as UITowerUpgradeView, tower, CanUpgradeTower, UpgradeTower);
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
            _towerUpgraderSelectPresentation.SetDeselected();
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
