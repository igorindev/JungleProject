using UnityEngine;

public abstract class ReturnToPool<T> : MonoBehaviour where T : Component
{
    T component;
    IObjectPool<T> pool;

    public void Setup(IObjectPool<T> objectPool)
    {
        pool = objectPool;
        component = GetComponent<T>();
    }

    void OnDisable()
    {
        pool.Return(component);
    }
}