using Events.DogEvents;
using Unity.AI.Navigation;
using UnityEngine;

public class LevelManager : GameManager
{
    [SerializeField]
    private NavMeshSurface surface;
    
    [SerializeField]
    private GameData gameData;

    private float _timeElapsed;
    [SerializeField]
    private Dog dog;
    
    private void Start()
    {
        gameData.Reset();
        surface.BuildNavMesh();
        dog.EventObserver.Subscribe(DogEventRaised);
    }

    private void FixedUpdate()
    {
        _timeElapsed += Time.fixedDeltaTime;
        gameData.time = FormatTime(_timeElapsed);
    }

    private void DogEventRaised(object sender, DogEvent e)
    {
        gameData.itemsInspected = e.EventType switch
        {
            DogEvent.Type.ItemInspected => dog.ItemsInspected,
            _ => gameData.itemsInspected
        };
    }

    private static string FormatTime(float time)
    {
        string minutes = $"{(int)(time / 60)}";
        string seconds = $"{(int)(time % 60)}";
        string milliseconds = $"{(int)(time * 100 % 100)}";

        return $"{Pad(minutes)}:{Pad(seconds)}:{Pad(milliseconds)}";

        string Pad(string s) => s.Length < 2 ? "0" + s : s;
    }
}
