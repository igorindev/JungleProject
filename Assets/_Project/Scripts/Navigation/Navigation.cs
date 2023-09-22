
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface INavigation
{
    bool CalculateIfPathAvailable();
    void Setup();
}

public class Navigation : MonoBehaviour, INavigation
{
    [SerializeField] Transform spawnPosition;
    [SerializeField] Transform targetPosition;
    [SerializeField] NavMeshData _navMeshData;
    [SerializeField] LayerMask _layerMask;

    NavMeshPath navMeshPath;
    List<NavMeshBuildSource> navMeshBuildSources = new List<NavMeshBuildSource>(); 

    public void Setup()
    {
        navMeshPath = new NavMeshPath();
        List<NavMeshBuildMarkup> markups = new List<NavMeshBuildMarkup>();
        NavMeshBuilder.CollectSources(_navMeshData.sourceBounds, _layerMask, NavMeshCollectGeometry.PhysicsColliders, 0, markups, navMeshBuildSources);
    }

    public bool CalculateIfPathAvailable()
    {
        //if (NavMeshBuilder.UpdateNavMeshData(_navMeshData, NavMesh.GetSettingsByIndex(0), navMeshBuildSources, _navMeshData.sourceBounds))
        //{
        //    Debug.Log("update");
        //}

        NavMesh.CalculatePath(spawnPosition.position, targetPosition.position, NavMesh.AllAreas, navMeshPath);
        return navMeshPath.status == NavMeshPathStatus.PathComplete;
    }
}