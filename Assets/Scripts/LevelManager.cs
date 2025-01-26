using Events.DogEvents;
using Unity.AI.Navigation;
using UnityEngine;

/// <summary>
/// Handles the functionality specific to the Level scene
/// </summary>
public class LevelManager : GameManager
{
    [SerializeField]
    private NavMeshSurface surface;
    
    [SerializeField]
    private GameData gameData;

    // value would be changed to a list if the game was expanded for multiple dogs
    [SerializeField]
    private Dog dog;
    
    private float _timeElapsed;
    
    private void Start()
    {
        // sets the time and item count back to 0
        gameData.Reset();
        
        // rebuilds the navmesh after items have been spawned in
        surface.BuildNavMesh();
        
        dog.EventObserver.Subscribe(DogEventRaised);
    }

    private void FixedUpdate()
    {
        // timer is only updated while the game is running
        _timeElapsed += Time.fixedDeltaTime;
        gameData.time = FormatTime(_timeElapsed);
    }

    private void DogEventRaised(object sender, DogEvent e)
    {
        // updates the UI when the dog inspects an item
        gameData.itemsInspected = e.EventType switch
        {
            DogEvent.Type.ItemInspected => dog.ItemsInspected,
            _ => gameData.itemsInspected
        };
    }

    /// <summary>
    /// Converts the time to a string in the format: <br/>
    /// "Minutes:Seconds:Milliseconds"
    /// </summary>
    /// <param name="time">time value to format</param>
    /// <returns>formatted string</returns>
    private static string FormatTime(float time)
    {
        string minutes = $"{(int)(time / 60)}";
        string seconds = $"{(int)(time % 60)}";
        string milliseconds = $"{(int)(time * 100 % 100)}";

        return $"{Pad(minutes)}:{Pad(seconds)}:{Pad(milliseconds)}";

        // each value is at least 2 digits
        string Pad(string s) => s.Length < 2 ? "0" + s : s;
    }
}
