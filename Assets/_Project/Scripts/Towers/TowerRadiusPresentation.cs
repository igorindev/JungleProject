using UnityEngine;

public interface ITowerRadiusPresentation
{
    void UpdateRadius(float radius);
}

public class TowerRadiusPresentation : ITowerRadiusPresentation
{
    readonly Transform _radiusObject;
    float _radius;

    public TowerRadiusPresentation(float radius, Transform radiusObject)
    {
        _radius = radius;
        _radiusObject = radiusObject;

        UpdateRadius(_radius);
    }

    public void UpdateRadius(float radius)
    {
        _radius = radius;
        _radiusObject.localScale = Vector3.one * _radius * 2;
    }
}
