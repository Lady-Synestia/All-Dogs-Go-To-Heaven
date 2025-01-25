using System;
using UnityEngine;
using Events.UIEvents;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private SpawnData spawnData;
    private SpawnData _spawnDataDefaults;
    
    private void Start()
    {
        _spawnDataDefaults = ScriptableObject.CreateInstance<SpawnData>();
        UIEventObserver.Instance.OnEvent += ButtonPressed;
    }

    private void ButtonPressed(object sender, UIEvent e)
    {
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
        }
    }
}