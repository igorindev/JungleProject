using UnityEngine;
using Object = UnityEngine.Object;

public abstract class PoolBase<T> where T : Component
{
    protected const int defaultCapacity = 10;
    const int maxPoolSize = 10;

    readonly protected T _prefab;

    readonly protected IObjectPool<T> pool;

    public IObjectPool<T> Pool => pool;

    public PoolBase(T prefab)
    {
        _prefab = prefab;
        pool = new ObjectPool<T>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, defaultCapacity, maxPoolSize);
    }

    protected abstract T CreatePooledItem();

    // If the pool capacity is reached then any items returned will be destroyed.
    // We can control what the destroy behavior does, here we destroy the GameObject.
    protected virtual void OnDestroyPoolObject(T obj)
    {
        Object.Destroy(obj.gameObject);
    }

    // Called when an item is returned to the pool using Release
    protected virtual void OnReturnedToPool(T obj)
    {
        obj.gameObject.SetActive(false);
    }

    // Called when an item is taken from the pool using Get
    protected virtual void OnTakeFromPool(T obj)
    {
        obj.gameObject.SetActive(true);
    }
}