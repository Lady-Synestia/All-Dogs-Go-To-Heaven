using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class GameSettings : MonoBehaviour
    {
        [SerializeField]
        private SpawnData spawnData;
        private SpawnData _spawnDefaults;
        
        private void Awake()
        {
            _spawnDefaults = ScriptableObject.CreateInstance<SpawnData>();
            
            VisualElement container = GetComponent<UIDocument>().rootVisualElement;
            
            Button backButton = container.Q<Button>("Back");
            backButton.RegisterCallback<MouseUpEvent>((evt) => ButtonActions.ChangeUI("GameSettings", "MainMenu"));
            
            Button playButton = container.Q<Button>("Play");
            playButton.RegisterCallback<MouseUpEvent>((evt) => ButtonActions.ChangeScene("Level"));
            
            Button resetButton = container.Q<Button>("Reset");
            resetButton.RegisterCallback<MouseUpEvent>((evt) => spawnData.SetToDefault(_spawnDefaults));

            container.visible = false;
        }
    }
}


