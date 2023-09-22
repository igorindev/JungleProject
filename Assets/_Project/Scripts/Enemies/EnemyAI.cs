using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    List<Enemy> spawnedEnemyList; 

    void Update()
    {
        for (int i = 0; i < spawnedEnemyList.Count; i++)
        {
            //spawnedEnemyList[i].Move();
        }
    }
}
