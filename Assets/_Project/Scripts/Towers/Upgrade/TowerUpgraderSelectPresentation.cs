using UnityEngine;

public interface ITowerUpgraderSelectPresentation
{
    void UpdateSelection();
    void SetDeselected();
    void SetSelected();
}

public class TowerUpgraderSelectPresentation : ITowerUpgraderSelectPresentation
{
    readonly Camera _cam;
    readonly LayerMask _towersMask;
    readonly Material _selectionMaterial;

    Tower _tower;
    Tower _selectedTower;

    public TowerUpgraderSelectPresentation(Camera cam, LayerMask towersMask, Material selectionMaterial)
    {
        _cam = cam;
        _towersMask = towersMask;
        _selectionMaterial = selectionMaterial;
    }

    public void SetDeselected()
    {
        Deselected(_selectedTower);
        _selectedTower = null;
    }

    void Deselected(Tower tower)
    {
        if (tower != null)
            tower.GetComponentInChildren<MeshRenderer>().sharedMaterial = tower.GetOriginalMaterial();
    }

    public void UpdateSelection()
    {
        if (HasUpgradableTowerAtPosition(out Tower tower))
        {
            tower.GetComponentInChildren<MeshRenderer>().sharedMaterial = _selectionMaterial;

            if (_tower != tower && _selectedTower != tower)
            {
                Deselected(_tower);
                _tower = tower;
            }
        }
        else
        {
            Deselected(_tower);
        }
    }

    public void SetSelected()
    {
        if (_tower != _selectedTower)
        {
            Deselected(_selectedTower);
            _selectedTower = _tower;
        }
        _tower = null;
    }

    bool HasUpgradableTowerAtPosition(out Tower tower)
    {
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
        tower = null;
        if (Physics.Raycast(ray, out RaycastHit hit, 10000, _towersMask, QueryTriggerInteraction.Ignore))
        {
            tower = hit.collider.GetComponent<Tower>();
        }

        return tower != null;
    }
}