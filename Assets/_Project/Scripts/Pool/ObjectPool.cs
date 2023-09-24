using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;

public interface IObjectPool<T>
{
    void Clear();
    T Get();
    void Return(T element);
}

public class ObjectPool<T> : IObjectPool<T> where T : Object
{
    readonly Func<T> _createPooledItem;
    readonly Action<T> _onTakeFromPool;
    readonly Action<T> _onReturnedToPool;
    readonly Action<T> _onDestroyPoolObject;
    readonly int _maxPoolSize;

    readonly Stack<T> pool;

    public ObjectPool(Func<T> createPooledItem, Action<T> onTakeFromPool, Action<T> onReturnedToPool, Action<T> onDestroyPoolObject, int defaultCapacity, int maxPoolSize)
    {
        _createPooledItem = createPooledItem;
        _onTakeFromPool = onTakeFromPool;
        _onReturnedToPool = onReturnedToPool;
        _onDestroyPoolObject = onDestroyPoolObject;
        _maxPoolSize = maxPoolSize;

        pool = new Stack<T>(defaultCapacity);
    }

    public void Clear()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            T pop = pool.Pop();
            _onDestroyPoolObject?.Invoke(pop);
        }

        pool.Clear();
    }

    public T Get()
    {
        T obj;
        if (pool.Count == 0)
        {
            obj = _createPooledItem?.Invoke();
        }
        else
        {
            obj = pool.Pop();
        }

        _onTakeFromPool?.Invoke(obj);
        return obj;
    }

    public void Return(T element)
    {
        pool.Push(element);
        _onReturnedToPool?.Invoke(element);
    }
}