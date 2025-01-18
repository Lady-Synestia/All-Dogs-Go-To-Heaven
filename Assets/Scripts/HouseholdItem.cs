using System.Collections.Generic;
using UnityEngine;

public struct TempData
{
    public int Strength;
    public int Range;
}


public class HouseholdItem : MonoBehaviour
{
    [Header("Visual Stimulus")]
    [Range(1, 10)]
    public int visualStrength = 1;
    [Range(0, 50)]
    public int visualRange = 0;
    
    [Header("Auditory Stimulus")]
    [Range(1, 10)]
    public int auditoryStrength = 1;
    [Range(0, 50)]
    public int auditoryRange = 0;

    [Header("Olfactory Stimulus")] 
    [Range(1, 10)]
    public int olfactoryStrength = 1;
    [Range(0, 50)]
    public int olfactoryRange = 0;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        NewStimulus(Stimulus.SenseType.Visual, visualRange, visualStrength);
        NewStimulus(Stimulus.SenseType.Auditory, auditoryRange, auditoryStrength);
        NewStimulus(Stimulus.SenseType.Olfactory, olfactoryRange, olfactoryStrength);
    }

    private void NewStimulus(Stimulus.SenseType sense, int range, int strength)
    {
        GameObject stimulusObject = new GameObject("VisualStimulus", typeof(Stimulus));
        stimulusObject.transform.SetParent(transform);
        stimulusObject.transform.localPosition = Vector3.zero;
        stimulusObject.GetComponent<Stimulus>().Create(sense, range, strength);
    }
}
