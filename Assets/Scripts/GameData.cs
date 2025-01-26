using UnityEngine;

/// <summary>
/// Stores the data for the current game session - reset when the level loads
/// </summary>
[CreateAssetMenu(fileName = "GameData", menuName = "Scriptable Objects/GameData")]
public class GameData : ScriptableObject
{
    // scriptable object allows UI labels to be bound to fields
    public int itemsInspected = 0;
    public string time = string.Empty;
    
    public void Reset()
    {
        itemsInspected = 0;
        time = string.Empty;
    }
}
