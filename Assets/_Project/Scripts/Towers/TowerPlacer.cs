using Collection;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface ITowerPlacer
{
    void Setup(IUIViewFactory uiViewFactory, IPlayerEconomy playerEconomy, INavigation navigation, IPlayerInput playerInput);
    void OnPlaceTower(Vector3 position);
    void OnSelectTower(TowerData data);
}

public class TowerPlacer : MonoBehaviour, ITowerPlacer
{
    [Header("PRESENTATION")]
    [SerializeField] Mesh _mesh;
    [SerializeField] Material _canPlace;
    [SerializeField] Material _canNotPlace;
    [SerializeField] TowerCollection _towerCollection;

    [Header("UI")]
    [SerializeField] UITowerView _towerPlacerView;
    [SerializeField] UITowerUpgradeView _towerUpgradeView;

    [Header("Masks")]
    [SerializeField] LayerMask checkCollisionWithEnemy;
    [SerializeField] LayerMask placeMask;
    [SerializeField] LayerMask upgradeMask;

    ITowerPlacerPresentation _towerPlacerPresentation;
    IInstantiator<Tower> _towerInstantiator;
    IPlayerEconomy _playerEconomy;
    INavigation _navigation;
    IPlayerInput _playerInput;
    IUIViewController _towerViewController;
    ITowerUpgrader _towerUpgrader;

    Camera _cam;

    readonly Action _placeTower;

    TowerData _currentSelectedTowerData;

    readonly Collider[] _colliders = new Collider[1];

    bool canPlace;
    Vector3 hitPoint;

    Action _onCompleteUpgrade;

    public void Setup(IUIViewFactory uiViewFactory, IPlayerEconomy playerEconomy, INavigation navigation, IPlayerInput playerInput)
    {
        _onCompleteUpgrade += ShowUI;
        _towerUpgrader = new TowerUpgrader(uiViewFactory, playerEconomy, playerInput, _towerUpgradeView, upgradeMask, _onCompleteUpgrade);

        _playerInput = playerInput;
        _playerInput.LeftMouseDown += PlaceTower;

        _playerEconomy = playerEconomy;
        _navigation = navigation;

        _cam = Camera.main;
        _towerPlacerPresentation = new TowerPlacerPresentation(_mesh, _canPlace, _canNotPlace);
        _towerInstantiator = new TowerInstantiator();

        _towerViewController = uiViewFactory.CreateTowerViewController(_towerPlacerView, _towerCollection.GetSize(), _towerCollection.GetFromCollection, OnSelectTower);

    }

    void OnDestroy()
    {
        _onCompleteUpgrade -= ShowUI;
        _playerInput.LeftMouseDown -= PlaceTower;
    }

    void Update()
    {
        if (_currentSelectedTowerData && !_playerInput.IsPointerOverUIObject())
        {
            canPlace = CanPlaceAtPosition(out hitPoint) && CanBuy();
            _towerPlacerPresentation.PresentPlacementPosition(canPlace, hitPoint);
        }
        else
            canPlace = false;
    }

    void PlaceTower()
    {
        if (canPlace)
        {
            OnPlaceTower(hitPoint);
        }
        else if (!_playerInput.IsPointerOverUIObject())
        {
            _towerViewController.Hide();
            _towerUpgrader.PickTower();
        }
    }

    void ShowUI()
    {
        _towerViewController.Show();
    }

    bool CanPlaceAtPosition(out Vector3 hitPoint)
    {
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 10000, placeMask, QueryTriggerInteraction.Ignore))
        {
            hitPoint = hit.point;
            if (Physics.OverlapSphereNonAlloc(hitPoint, 2, _colliders, checkCollisionWithEnemy, QueryTriggerInteraction.Collide) > 0)
            {
                return false;
            }

            return true;
        }

        hitPoint = Vector3.zero;
        return false;
    }

    bool CanBuy()
    {
        return _playerEconomy.CanUseCoins(_currentSelectedTowerData.TowerCost);
    }

    public void OnSelectTower(TowerData data)
    {
        _currentSelectedTowerData = data;
        Time.timeScale = data ? 0 : 1;
    }

    public void OnPlaceTower(Vector3 position)
    {
        Tower instance = _towerInstantiator.Spawn(_currentSelectedTowerData.Tower, position, Quaternion.identity);
        StartCoroutine(WaitNavMeshUpdate(instance, _currentSelectedTowerData));
    }

    IEnumerator WaitNavMeshUpdate(Tower instance, TowerData data)
    {
        yield return null; //Wait navmesh rebuild

        if (_navigation.CalculateIfPathAvailable())
        {
            _playerEconomy.RemoveCoins(_currentSelectedTowerData.TowerCost);
            _placeTower?.Invoke();
            instance.Setup(data);
        }
        else
        {
            Debug.Log("Can't place, will block path!");
            Destroy(instance.gameObject);
        }
    }
}
