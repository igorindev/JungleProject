using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyData : ScriptableObject
{
    [SerializeField] Enemy _enemy;
    [SerializeField] string _enemyName;
    [SerializeField] float _enemySpeed;

    public Enemy Prefab { get => _enemy; }
    public string EnemyName { get => _enemyName; }
    public float EnemySpeed { get => _enemySpeed; }
}
