using UnityEngine;

public class ItemFactory : MonoBehaviour
{

    [Header("Spawner Settings")] [SerializeField] [Range(0, 20)]
    private int numberOfItems = 5;
    
    [Header("Visual Weightings")]
    [SerializeField] [Range(0, 100)] private int visualChance = 100;
    [SerializeField] [Range(0, 10)] private int visualStrengthWeight = 5;
    [SerializeField] [Range(0, 10)] private int visualRangeWeight = 5;
    [Header("Auditory Weightings")]
    [SerializeField] [Range(0, 100)] private int auditoryChance = 100;
    [SerializeField] [Range(0, 10)] private int auditoryStrengthWeight = 5;
    [SerializeField] [Range(0, 10)] private int auditoryRangeWeight = 5;
    [Header("Olfactory Weightings")]
    [SerializeField] [Range(0, 100)] private int olfactoryChance = 100;
    [SerializeField] [Range(0, 10)] private int olfactoryStrengthWeight = 5;
    [SerializeField] [Range(0, 10)] private int olfactoryRangeWeight = 5;

    private const int SpawnRange = 24;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        for (int i = 0; i < numberOfItems; i++)
        {
            GameObject itemObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            itemObject.name = "Item " + i;
            itemObject.transform.SetParent(transform);
            int x = Random.Range(-SpawnRange, SpawnRange);
            int z = Random.Range(-SpawnRange, SpawnRange);
            itemObject.transform.localPosition = new Vector3(x, 3, z);
            Item item = itemObject.AddComponent<Item>();

            if (visualChance == 100 || Random.Range(0, 100) <= visualChance)
            { 
                item.AddStimulus(new StimulusData(Stimulus.SenseType.Visual, Generate(visualStrengthWeight), Generate(visualRangeWeight)));
            }
            
            if (auditoryChance == 100 || Random.Range(0, 100) <= auditoryChance)
            { 
                item.AddStimulus(new StimulusData(Stimulus.SenseType.Auditory, Generate(auditoryStrengthWeight), Generate(auditoryRangeWeight)));
            }
            
            if (olfactoryChance == 100 || Random.Range(0, 100) <= olfactoryChance)
            { 
                item.AddStimulus(new StimulusData(Stimulus.SenseType.Olfactory, Generate(olfactoryStrengthWeight), Generate(olfactoryRangeWeight)));
            }
        }
    }

    private static int Generate(int weight)
    {
        int value = Random.Range(0, 5) * weight;
        return value;
    }
}
