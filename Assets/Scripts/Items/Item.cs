using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

namespace Items
{
    /// <summary>
    /// Items that can be spawned in the world. Each item can have one each of visual, auditory, and olfactory stimuli
    /// </summary>
    public class Item : MonoBehaviour
    {
        // used to prevent adding multiple of the same types of stimulus
        public Dictionary<Stimulus.SenseType, StimulusData> Stimuli { get; } = new();
        
        public bool Inspected { get; set; }
        
        // Reference needed in order for the shader to be included in the build - set by the factory
        public Shader StimulusOutlineShader { get; set;}
    
        private void OnEnable()
        {
            // Items are created as primitive cubes, their collider is disabled to stop the dog getting stuck on them
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        
        /// <summary>
        /// Creates a new stimulus game object and attaches it to the item.
        /// </summary>
        /// <param name="data">Data object to create the stimulus from</param>
        public void AddStimulus(StimulusData data)
        {
            if (Stimuli.TryAdd(data.Sense, data))
            {
                // Creates a stimulus object and attaches it to the item 
                GameObject stimulusObject = new ($"{data.Sense} Stimulus", typeof(Stimulus));
                stimulusObject.transform.SetParent(transform);
                stimulusObject.transform.localPosition = Vector3.zero;
                
                // setting up the stimulus based on the data provided
                stimulusObject.GetComponent<Stimulus>().Create(this, data);
            }
        }
    }

}

