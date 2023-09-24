using Collection;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public interface ITowerPlacer
{
    void Setup(IUIViewFactory uiViewFactory, IPlayerEconomy playerEconomy, INavigation navigation, IPlayerInput playerInput);
    void OnPlaceTower(Vector3 position);
    void OnSelectTower(TowerData data);
}

public class TowerPlacer : MonoBehaviour, ITowerPlacer
{
    [Header("PLACEMENT")]
    [SerializeField] float _overlapRadiusCheck = 2;

    [Header("PRESENTATION")]
    [SerializeField] Mesh _mesh;
    [SerializeField] Material _canPlaceMaterial;
    [SerializeField] Material _canNotPlaceMaterial;
    [SerializeField] Material _selectMaterial;
    [SerializeField] Material _deselectMaterial;
    [SerializeField] TowerCollection _towerCollection;

    [Header("UI")]
    [SerializeField] UITowerView _towerPlacerView;
    [SerializeField] UITowerUpgradeView _towerUpgradeView;

    [Header("Masks")]
    [SerializeField] LayerMask _avoidOverlapCheck;
    [SerializeField] LayerMask _placeMask;
    [SerializeField] LayerMask _upgradeMask;

    ITowerPlacerPresentation _towerPlacerPresentation;
    IInstantiator<Tower> _towerInstantiator;
    IPlayerEconomy _playerEconomy;
    INavigation _navigation;
    IPlayerInput _playerInput;
    IUIViewController _towerViewController;
    ITowerUpgrader _towerUpgrader;

    Camera _cam;

    TowerData _currentSelectedTowerData;

    Action _onCompleteUpgrade;
    readonly Collider[] _placementCheckCollider = new Collider[1];

    bool _canPlaceTowerAtPosition;
    Vector3 _placementPosition;

    public void Setup(IUIViewFactory uiViewFactory, IPlayerEconomy playerEconomy, INavigation navigation, IPlayerInput playerInput)
    {
        _onCompleteUpgrade += ShowUI;
        _towerUpgrader = new TowerUpgrader(uiViewFactory, playerEconomy, playerInput, _selectMaterial, _deselectMaterial, _towerUpgradeView, _upgradeMask, _onCompleteUpgrade);

        _playerInput = playerInput;
        _playerInput.LeftMouseDown += PlaceTower;

        _playerEconomy = playerEconomy;
        _navigation = navigation;

        _cam = Camera.main;
        _towerPlacerPresentation = new TowerPlacerPresentation(_mesh, _canPlaceMaterial, _canNotPlaceMaterial);
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
        if (!_playerInput.IsPointerOverUIObject())
        {
            if (_currentSelectedTowerData)
            {
                _canPlaceTowerAtPosition = CanPlaceAtPosition(out _placementPosition) && CanBuy();
                _towerPlacerPresentation.PresentPlacementPosition(_canPlaceTowerAtPosition, _placementPosition);
            }
            else
            {
                _canPlaceTowerAtPosition = false;
                _towerUpgrader.UpdateSelectTower();
            }
            return;
        }

        _canPlaceTowerAtPosition = false;
    }

    void PlaceTower()
    {
        if (_canPlaceTowerAtPosition)
        {
            OnPlaceTower(_placementPosition);
        }
        else if (_currentSelectedTowerData == false && !_playerInput.IsPointerOverUIObject())
        {
            if (_towerUpgrader.PickTower())
                _towerViewController.Hide();
        }
    }

    void ShowUI()
    {
        _towerViewController.Show();
    }

    bool CanPlaceAtPosition(out Vector3 hitPoint)
    {
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 10000, _placeMask, QueryTriggerInteraction.Ignore))
        {
            hitPoint = hit.point;
            if (Physics.OverlapSphereNonAlloc(hitPoint, _overlapRadiusCheck, _placementCheckCollider, _avoidOverlapCheck, QueryTriggerInteraction.Collide) > 0)
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

    bool navMeshUpdating = false;
    public void OnPlaceTower(Vector3 position)
    {
        if (navMeshUpdating) return;
        navMeshUpdating = true;
        NavMesh.onPreUpdate = WaitNavMeshStartUpdate;

        Tower instance = _towerInstantiator.Spawn(_currentSelectedTowerData.Tower, position, Quaternion.identity);
        StartCoroutine(WaitNavMeshUpdate(instance, _currentSelectedTowerData));

    }

    IEnumerator WaitNavMeshUpdate(Tower instance, TowerData data)
    {
        while (navMeshUpdating)
        {
            yield return null;
        }

        yield return null;

        if (_navigation.CalculateIfPathAvailable())
        {
            _playerEconomy.RemoveCoins(_currentSelectedTowerData.TowerCost);
            instance.Setup(data);
        }
        else
        {
            Debug.Log("Can't place, will block path!");
            Destroy(instance.gameObject);
        }
    }

    void WaitNavMeshStartUpdate()
    {
        navMeshUpdating = false;
    }
}
