using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : PoolMember<T>
{
    readonly Queue<T> inactiveGO = new Queue<T>();

    T _prefab;

    public Pool(T prefab)
    {
        _prefab = prefab;
    }

    public T GetFromPool()
    {
        if (inactiveGO.Count == 0)
        {
            T poolMember = Object.Instantiate(_prefab);
            poolMember.Setup(this);
            return poolMember;
        }

        return inactiveGO.Dequeue();
    }

    public void RecicleBackToPool(PoolMember<T> instance)
    {
        inactiveGO.Enqueue((T)instance);
    }
}

public class PoolMember<T> : MonoBehaviour where T : PoolMember<T>
{
    Pool<T> _myPool;

    public void Setup(Pool<T> pool)
    {
        _myPool = pool;
    }

    public void Recicle()
    {
        _myPool.RecicleBackToPool(this);
    }
}
    