using UnityEngine;

public interface IInstantiator<T> where T : MonoBehaviour
{
    T Spawn(T gameObject, Vector3 position, Quaternion rotation);
}

public class TowerInstantiator : IInstantiator<Tower>
{
    public Tower Spawn(Tower gameObject, Vector3 position, Quaternion rotation)
    {
        return Object.Instantiate(gameObject, position, rotation);
    }
}
