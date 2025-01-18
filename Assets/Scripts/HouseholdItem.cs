using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HouseholdItem : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        NewStimulus(Stimulus.SenseType.Visual, 10, 10);
        NewStimulus(Stimulus.SenseType.Auditory, 5, 5);
        NewStimulus(Stimulus.SenseType.Olfactory, 20, 15);
    }

    private void NewStimulus(Stimulus.SenseType sense, int range, int strength)
    {
        GameObject stimulusObject = new GameObject("VisualStimulus", typeof(Stimulus));
        stimulusObject.transform.SetParent(transform);
        stimulusObject.transform.localPosition = Vector3.zero;
        stimulusObject.GetComponent<Stimulus>().Create(sense, range, strength);
    }
}
