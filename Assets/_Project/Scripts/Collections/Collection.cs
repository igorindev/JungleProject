using UnityEngine;

namespace Collection
{
    public class Collection<T> : ScriptableObject where T : Object
    {
        [SerializeField] T[] group;

        public T GetFromCollection(int index)
        {
            return group[index];
        }

        public T GetRandomFromCollection()
        {
            return group[Random.Range(0, group.Length)];
        }

        public int GetSize()
        {
            return group.Length;
        }
    }
}