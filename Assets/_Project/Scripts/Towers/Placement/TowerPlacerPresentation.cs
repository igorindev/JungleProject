using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITowerPlacerPresentation
{
    void PresentPlacementPosition(bool ableToPlace, Vector3 target);
}

public class TowerPlacerPresentation : ITowerPlacerPresentation
{
    readonly Mesh _mesh;
    readonly Material _canPlace;
    readonly Material _canNotPlace;

    public TowerPlacerPresentation(Mesh mesh, Material canPlace, Material canNotPlace)
    {
        _mesh = mesh;
        _canPlace = canPlace;
        _canNotPlace = canNotPlace;
    }

    public void PresentPlacementPosition(bool ableToPlace, Vector3 target)
    {
        Mesh meshToDraw = _mesh;
        Material materialToDraw;
        if (ableToPlace)
        {
            materialToDraw = _canPlace;
        }
        else
        {
            materialToDraw = _canNotPlace;
        }

        Graphics.DrawMesh(meshToDraw, target, Quaternion.identity, materialToDraw, 0);
    }
}
