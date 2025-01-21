using UnityEngine;

public record StimulusData(Stimulus.SenseType Sense, int Strength, int Range, int DecayRate = 0)
{
    public Stimulus.SenseType Sense { get; } = Sense;
    public int Strength { get; set; } = Strength;
    public int Range { get; } = Range;
    public int DecayRate { get; set; } = DecayRate;
}

public class Stimulus : MonoBehaviour
{
    public enum SenseType
    {
        Visual,
        Auditory,
        Olfactory,
    }
    
    public StimulusData Data { get; private set; }
    public bool HasDecay { get; private set; }
    private int _timeSinceLastDecay;
    private SphereCollider _trigger;
    private void Awake()
    {
        _trigger = gameObject.AddComponent<SphereCollider>();
        gameObject.tag = "Stimulus";
        _trigger.isTrigger = true;
    }

    public void Create(StimulusData data)
    {
        Data = data;
        HasDecay = data.DecayRate > 0;
        _trigger.radius = data.Range;
    }

    public void Decay()
    {
        if (HasDecay)
        {
            _timeSinceLastDecay = Mathf.RoundToInt(Time.time - _timeSinceLastDecay);
            Data.Strength -= Data.DecayRate * _timeSinceLastDecay;;
            _trigger.radius -= Data.DecayRate * _timeSinceLastDecay;;

            if (Data.Strength <= 0)
            {
                Data.Strength = 0;
                Data.DecayRate = 0;
                HasDecay = false;
            }
        }
    }

    public void Encountered()
    {
        _trigger.enabled = false;
    }

    public bool Inspected()
    {
        Item item = transform.parent.GetComponent<Item>();
        return item.Inspected;
    }

    public void Inspect()
    {
        Item item = transform.parent.GetComponent<Item>();
        item.Inspected = true;
        item.gameObject.SetActive(false);
    }
}
