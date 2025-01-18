using System;
using UnityEngine;
using Events;

public class Stimulus : MonoBehaviour
{
    public enum SenseType
    {
        Visual,
        Auditory,
        Olfactory,
    }
    
    [SerializeField]
    public int Range { get; private set; }
    [SerializeField]
    public int Strength { get; private set; }
    public SenseType Sense { get; private set; }
    public bool HasDecay { get; private set; }
    private int _decayRate;
    private int _timeSinceLastDecay;
    private SphereCollider _trigger;
    private void Awake()
    {
        _trigger = this.gameObject.AddComponent<SphereCollider>();
        _trigger.isTrigger = true;
    }

    public void Create(SenseType type, int range, int strength, int decayRate=0)
    {
        Range = range;
        Strength = strength;
        Sense = type;
        HasDecay = decayRate > 0;
        _decayRate = decayRate;
        _trigger.radius = range;
    }

    public void Decay()
    {
        if (HasDecay)
        {
            _timeSinceLastDecay = Mathf.RoundToInt(Time.time - _timeSinceLastDecay);
            Strength -= _decayRate * _timeSinceLastDecay;;
            _trigger.radius -= _decayRate * _timeSinceLastDecay;;

            if (Strength <= 0)
            {
                Strength = 0;
                _decayRate = 0;
                HasDecay = false;
            }
        }
    }
}
