using Collection;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface ITowerPlacer
{
    void Setup(IUIViewFactory uiViewFactory, IPlayerEconomy playerEconomy, INavigation navigation);
    void OnPlaceTower(Vector3 position);
    void OnSelectTower(TowerData data, Action action);
}

public class TowerPlacer : MonoBehaviour, ITowerPlacer
{
    [SerializeField] Mesh _mesh;
    [SerializeField] Material _canPlace;
    [SerializeField] Material _canNotPlace;
    [SerializeField] TowerCollection _towerCollection;
    [SerializeField] UITowerView _towerPlacerView;
    [SerializeField] LayerMask checkCollisionWithEnemy;
    [SerializeField] LayerMask placeMask;

    ITowerPlacerPresentation _towerPlacerPresentation;
    IInstantiator<Tower> _towerInstantiator;
    IPlayerEconomy _playerEconomy;
    INavigation _navigation;

    Camera _cam;

    Action<TowerData> _selectTower;
    Action _tryPlaceTower;
    Action _placeTower;

    TowerData _currentSelectedTowerData;

    Collider[] _colliders = new Collider[1];

    List<RaycastResult> results = new List<RaycastResult>();

    void Update()
    {
        if (_currentSelectedTowerData && !IsPointerOverUIObject())
        {
            bool canPlace = CanPlaceAtPosition(out Vector3 hitPoint) && CanBuy();

            _towerPlacerPresentation.PresentPlacementPosition(canPlace, hitPoint);

            if (canPlace && Input.GetMouseButtonDown(0))
            {
                OnPlaceTower(hitPoint);
            }
        }
    }

    public void Setup(IUIViewFactory uiViewFactory, IPlayerEconomy playerEconomy, INavigation navigation)
    {
        _playerEconomy = playerEconomy;
        _navigation = navigation;

        _cam = Camera.main;
        _towerPlacerPresentation = new TowerPlacerPresentation(_mesh, _canPlace, _canNotPlace);
        _towerInstantiator = new TowerInstantiator();

        UITowerView view = Instantiate(_towerPlacerView);
        UITowerViewController viewController = new UITowerViewController(view, _towerCollection.GetSize(), _towerCollection.GetFromCollection, OnSelectTower);
        viewController.Present();
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

    public bool IsPointerOverUIObject()
    {
        results.Clear();

        Vector3 activeTouches = Input.mousePosition;

        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = activeTouches;

        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    bool CanBuy()
    {
        return _playerEconomy.CanUseCoins(_currentSelectedTowerData.TowerCost);
    }

    public void OnSelectTower(TowerData data, Action _)
    {
        //_placeTower = cancelSelection;
        _currentSelectedTowerData = data;
        Time.timeScale = data ? 0 : 1;
    }

    public void OnPlaceTower(Vector3 position)
    {
        Tower instance = _towerInstantiator.Spawn(_currentSelectedTowerData.Tower, position, Quaternion.identity);
        StartCoroutine(WaitNavMeshUpdate(instance));
    }

    IEnumerator WaitNavMeshUpdate(Tower instance)
    {
        yield return null; //Wait navmesh rebuild

        if (_navigation.CalculateIfPathAvailable())
        {
            _playerEconomy.RemoveCoins(_currentSelectedTowerData.TowerCost);
            _placeTower?.Invoke();
            instance.Setup();
        }
        else
        {
            Debug.Log("Can't place, will block path!");
            Destroy(instance.gameObject);
        }
    }
}
