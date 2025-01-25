using Unity.AI.Navigation;
using UnityEngine;

public class LevelManager : GameManager
{
    [SerializeField]
    private NavMeshSurface surface;
    
    private void Start()
    {
        surface.BuildNavMesh();
    }
}
