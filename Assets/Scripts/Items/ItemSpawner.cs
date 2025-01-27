using UnityEngine;

namespace Items
{
    /// <summary>
    /// Factory Class to spawn Items in the world. Uses the Spawning Data to randomly generate stimuli for the items.
    /// </summary>
    public class ItemFactory : MonoBehaviour
    {
        // Reference to the scriptable object
        [SerializeField]
        private SpawnData spawnData;
        
        // Reference needed in order for the shader to be included in the build - passed to the item objects.
        [SerializeField] private Shader outlineShader;
        
        private const int SpawnRange = 24;
        
        private void Awake()
        {
            for (int i = 0; i < spawnData.spawnCount; i++)
            {
                // Creates item objects in random positions throughout the world
                GameObject itemObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
                itemObject.name = "Item " + i;
                itemObject.transform.SetParent(transform);
                itemObject.transform.localPosition = GenerateSpawnPosition();
                
                // setting up the item object
                Item item = itemObject.AddComponent<Item>();
                item.StimulusOutlineShader = outlineShader;
                
                item.AddStimulus(GenerateStimulusData(Stimulus.SenseType.Visual, spawnData.visualChance, spawnData.visualStrengthWeight, spawnData.visualRangeWeight));
                item.AddStimulus(GenerateStimulusData(Stimulus.SenseType.Auditory, spawnData.auditoryChance, spawnData.auditoryStrengthWeight, spawnData.auditoryRangeWeight));
                item.AddStimulus(GenerateStimulusData(Stimulus.SenseType.Olfactory, spawnData.olfactoryChance, spawnData.olfactoryStrengthWeight, spawnData.olfactoryRangeWeight));
                
            }
        }

        /// <summary>
        /// Generates a random spawn position within the bounds of the scene
        /// </summary>
        /// <returns>Vector3 position for the item</returns>
        private static Vector3 GenerateSpawnPosition()
        {
            int x = Random.Range(-SpawnRange, SpawnRange);
            int z = Random.Range(-SpawnRange, SpawnRange);

            return new Vector3(x, 1, z);
        }

        /// <summary>
        /// Instantiates a stimulus data object based on the chance and weights provided.
        /// </summary>
        /// <param name="sense">Type of stimulus to create</param>
        /// <param name="chance">Chance in percent of creating a stimulus</param>
        /// <param name="strengthWeight">Weighting (1-10) for how strong the stimulus should be</param>
        /// <param name="rangeWeight">Weighting (1-10) for the size of the stimulus' range</param>
        /// <returns></returns>
        private static StimulusData GenerateStimulusData(Stimulus.SenseType sense, int chance, int strengthWeight, int rangeWeight)
        {
            if (chance == 100 || Random.Range(0, 100) <= chance)
            {
                return new StimulusData(sense, GenerateFromWeight(strengthWeight), GenerateFromWeight(rangeWeight));
            }
            // returns an empty object if the stimulus isn't created
            return new StimulusData(sense, 0, 0);
        }
        
        // generates a strength or range value in the range 1-50
        private static int GenerateFromWeight(int weight) => Random.Range(1, 5) * weight;

        
    }

}

