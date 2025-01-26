using UnityEngine;

public class ItemFactory : MonoBehaviour
{
    [SerializeField]
    private SpawnData spawnData;
    
    [SerializeField]
    private Shader outlineShader;

    private const int SpawnRange = 24;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        for (int i = 0; i < spawnData.spawnCount; i++)
        {
            GameObject itemObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            itemObject.name = "Item " + i;
            itemObject.transform.SetParent(transform);
            int x = Random.Range(-SpawnRange, SpawnRange);
            int z = Random.Range(-SpawnRange, SpawnRange);
            itemObject.transform.localPosition = new Vector3(x, 1, z);
            Item item = itemObject.AddComponent<Item>();
            item.StimulusOutlineShader = outlineShader;
            
            item.AddStimulus(GenerateStimulusData(Stimulus.SenseType.Visual, spawnData.visualChance, spawnData.visualStrengthWeight, spawnData.visualRangeWeight));
            item.AddStimulus(GenerateStimulusData(Stimulus.SenseType.Auditory, spawnData.auditoryChance, spawnData.auditoryStrengthWeight, spawnData.auditoryRangeWeight));
            item.AddStimulus(GenerateStimulusData(Stimulus.SenseType.Olfactory, spawnData.olfactoryChance, spawnData.olfactoryStrengthWeight, spawnData.olfactoryRangeWeight));
            
        }
    }

    private static StimulusData GenerateStimulusData(Stimulus.SenseType sense, int chance, int strengthWeight, int rangeWeight)
    {
        if (chance == 100 || Random.Range(0, 100) <= chance)
        {
            return new StimulusData(sense, Generate(strengthWeight), Generate(rangeWeight));
        }
        return new StimulusData(sense, 0, 0);
    }

    private static int Generate(int weight) => Random.Range(0, 5) * weight;
}
