using System;
using UnityEngine;
using Events.UIEvents;
using UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Provides common functionality for every scene. Controls UI and responds to UI events
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] private SpawnData spawnData;
    private SpawnData _spawnDataDefaults;
    
    [SerializeField] private GameObject uiParent;

    private void Awake()
    {
        // creates the singleton UIEventObserver instance
        gameObject.AddComponent<UIEventObserver>();
        UIEventObserver.Instance.Subscribe(OnUIEvent);
        
        // saves the default data values so they can be reset if necessary
        _spawnDataDefaults = ScriptableObject.CreateInstance<SpawnData>();
        
        // Loads the UI elements of the scene
        foreach (UIElement element in uiParent.GetComponentsInChildren<UIElement>())
        {
            element.Load();
        }
    }

    /// <summary>
    /// Handles Scene-wide changes in response to UI Events
    /// </summary>
    protected void OnUIEvent(object sender, UIEvent e)
    {
        // subscribed to the OnEvent handler of the UIEventObserver
        switch (e.EventType)
        {
            case UIEvent.Type.Pause:
                Time.timeScale = 0;
                break;
            
            case UIEvent.Type.Resume:
                Time.timeScale = 1;
                break;
            
            case UIEvent.Type.Quit:
                Application.Quit();
                break;
            
            case UIEvent.Type.ResetData:
                spawnData.SetData(_spawnDataDefaults);
                break;
            
            case UIEvent.Type.ChangeScene:
                var args = (UIEventArgs)e.Args;
                Time.timeScale = 1;
                SceneManager.LoadScene(args.SceneTarget);
                break;
            
            case UIEvent.Type.GameEnd:
                Time.timeScale = 0;
                break;
        }
    }
}