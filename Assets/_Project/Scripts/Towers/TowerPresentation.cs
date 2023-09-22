using UnityEngine;

public interface ITowerPresentation
{
    void Show(GameObject gameObject);
}

public class TowerPresentation : ITowerPresentation
{
    public void Show(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }
}