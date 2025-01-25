using System;
using System.Collections;
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
    private LineRenderer _outline;
    private Material _material;
    private void Awake()
    {
        gameObject.tag = "Stimulus";
        
        _trigger = gameObject.AddComponent<SphereCollider>();
        _trigger.isTrigger = true;
        
        _outline = gameObject.AddComponent<LineRenderer>();
        _outline.widthMultiplier = 0.05f;
        _outline.loop = true;
        _outline.useWorldSpace = false;
        _outline.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        _outline.generateLightingData = true;
        _outline.receiveShadows = false;
        
        _material = new Material(Shader.Find("Unlit/Color"));
        
        _outline.enabled = false;
    }

    public void Create(StimulusData data)
    {
        Data = data;
        HasDecay = data.DecayRate > 0;
        _trigger.radius = data.Range;
        if (data.Range > 0)
        {
            _outline.positionCount = data.Range * 28;
            _outline.SetPositions(DrawCircle(data.Range));
            _material.SetColor("_Color", new Color((float)Data.Strength/10, 1, 0.2f, 1));
            _outline.material = _material;
        }
        _outline.enabled = false;
    }

    public void Decay()
    {
        if (HasDecay)
        {
            _timeSinceLastDecay = Mathf.RoundToInt(Time.time - _timeSinceLastDecay);
            Data.Strength -= Data.DecayRate * _timeSinceLastDecay;;

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
        StartCoroutine(ShowRange());
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

    private IEnumerator ShowRange()
    {
        _outline.enabled = true;
        const int steps = 20;
        yield return new WaitForSeconds(0.5f);
        for (float g = 1; g >= 0.4f; g -= 0.6f/steps)
        {
            _material.SetColor("_Color", new Color((float)Data.Strength/10, g, 0.2f, 1));
            _outline.material = _material;
            yield return new WaitForSeconds(3f/steps);
        }
        //_outline.enabled = false;
    }
    
    private static Vector3[] DrawCircle(int r = 1)
    {
        float step = r/5f;
        int seg = r * 28;
        Vector3[] points = new Vector3[seg];
        float x = 1f;
        float y = 0f;
        for (int i = 0; i <= seg/4; i++)
        {
            if (Mathf.Approximately(y, x))
            {
                x -= step;
                y = Circle(x);
            }
            if (y < x)
            {
                x = Circle(y);
                points[i] = new Vector3(x, 0f, y);
                y += step;
                
            }
            else
            {
                y = Circle(x);
                points[i] = new Vector3(x, 0f, y);
                x -= step;
            }
        }

        for (int i = (seg/4) + 1; i <= seg/2; i++)
        {
            points[i] = Vector3.Scale(points[(seg/2) - i], new Vector3(-1, 1, 1));
        }
        
        for (int i = (seg/2) + 1; i < seg; i++)
        {
            points[i] = Vector3.Scale(points[seg - i], new Vector3(1, 1, -1));
        }
        
        return points;

        float Circle(float a) => Mathf.Sqrt((r * r) - (a*a));
    }
}
