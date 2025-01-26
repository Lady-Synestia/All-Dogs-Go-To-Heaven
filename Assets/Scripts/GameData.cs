using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Scriptable Objects/GameData")]
public class GameData : ScriptableObject
{
    public int itemsInspected = 0;
    public string time = string.Empty;

    public void Reset()
    {
        itemsInspected = 0;
        time = string.Empty;
    }

    
}
