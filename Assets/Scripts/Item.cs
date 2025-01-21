using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Dictionary<Stimulus.SenseType, StimulusData> Stimuli = new();
    public bool Inspected { get; set; }
    
    public void AddStimulus(StimulusData data)
    {
        if (Stimuli.TryAdd(data.Sense, data))
        {
            GameObject stimulusObject = new ($"{data.Sense} Stimulus", typeof(Stimulus));
            stimulusObject.transform.SetParent(transform);
            stimulusObject.transform.localPosition = Vector3.zero;
            stimulusObject.GetComponent<Stimulus>().Create(data);
        }
    }
}
