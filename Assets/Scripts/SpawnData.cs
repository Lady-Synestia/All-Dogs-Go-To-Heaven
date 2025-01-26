using System;
using UnityEngine;

/// <summary>
/// Values used by the Item Factory to determine the properties of the items spawned.
/// Bound to the sliders of the GameSettings UI Element so values can be edited by the player.
/// </summary>
[CreateAssetMenu(fileName = "SpawnData", menuName = "Scriptable Objects/SpawnData")]
public class SpawnData : ScriptableObject
{
    [Header("Spawner Settings")]
    [Range(1, 50)]
    public int spawnCount = 15;

    [Range(0, 100)]
    public int visualChance = 50;
    [Range(0, 100)]
    public int auditoryChance = 40;
    [Range(0, 100)]
    public int olfactoryChance = 65;
    
    [Header("Advanced")]
    [Range(0, 10)]
    public int visualStrengthWeight = 2;
    [Range(0, 10)]
    public int visualRangeWeight = 10;
    [Range(0, 10)]
    public int auditoryStrengthWeight = 5; 
    [Range(0, 10)]
    public int auditoryRangeWeight = 7;
    [Range(0, 10)]
    public int olfactoryStrengthWeight = 10;
    [Range(0, 10)]
    public int olfactoryRangeWeight = 5;
    
    private void Awake()
    {
        // allows data to persist after a level is completed
        DontDestroyOnLoad(this);
    }
    
    public void SetData(SpawnData data)
    {
        // used to set the data back to its default values
        // could be used to set the data from another custom SpawnData object
        spawnCount = data.spawnCount;
        visualChance = data.visualChance;
        auditoryChance = data.auditoryChance;
        olfactoryChance = data.olfactoryChance;
        visualStrengthWeight = data.visualStrengthWeight;
        visualRangeWeight = data.visualRangeWeight;
        auditoryStrengthWeight = data.auditoryStrengthWeight;
        auditoryRangeWeight = data.auditoryRangeWeight;
        olfactoryStrengthWeight = data.olfactoryStrengthWeight;
        olfactoryRangeWeight = data.olfactoryRangeWeight;
    }
}
