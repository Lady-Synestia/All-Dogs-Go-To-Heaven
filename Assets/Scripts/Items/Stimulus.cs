using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Items
{
    
    public record StimulusData(Stimulus.SenseType Sense, int Strength, int Range, int DecayRate = 0)
    {
        // used to define the parameters of a stimulus.
        // record class is reference based - changes made to the data by the stimulus will be available to the item holding it
        public Stimulus.SenseType Sense { get; } = Sense;
        public int Strength { get; set; } = Strength;
        public int Range { get; } = Range;
        
        // Decay rate defaults to 0 as it is not used
        public int DecayRate { get; set; } = DecayRate;
    }
    
    /// <summary>
    /// Class for a stimulus, can be visual, auditory, or olfactory
    /// </summary>
    public class Stimulus : MonoBehaviour
    {
        public enum SenseType
        {
            Visual,
            Auditory,
            Olfactory,
        }
        
        private Item _item;
     
        // Data supplied by the item the stimulus is part of
        public StimulusData Data { get; private set; }
        
        // stimulus decay is not used.
        public bool HasDecay { get; private set; }
        private int _timeSinceLastDecay;
        
        // component references
        private SphereCollider _trigger;
        private LineRenderer _outline;
        private Material _material;
        
        private bool _focused;
        
        private void Awake()
        {
            // setting up the game object and its components.
            gameObject.tag = "Stimulus";
            
            _trigger = gameObject.AddComponent<SphereCollider>();
            _trigger.isTrigger = true;
            
            _outline = gameObject.AddComponent<LineRenderer>();
            _outline.widthMultiplier = 0.05f;
            _outline.loop = true; // completes the circle
            _outline.enabled = false; // line not shown until the stimulus is encountered
            
            // other line renderer parameters - tested in the editor
            _outline.useWorldSpace = false;
            _outline.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            _outline.generateLightingData = true;
            _outline.receiveShadows = false;
        }
    
        /// <summary>
        /// Creating a new stimulus based on specified data
        /// </summary>
        /// <param name="item">Item that the stimulus is a part of</param>
        /// <param name="data">Data to create the stimulus from</param>
        public void Create(Item item, StimulusData data)
        {
            _item = item;
            Data = data;
            HasDecay = data.DecayRate > 0;
            
            _trigger.radius = data.Range;
            
            // only completes setup if the stimulus has a range
            if (data.Range > 0)
            {
                _outline.positionCount = data.Range * 28;
                _outline.SetPositions(DrawCircle(data.Range));
                _material = new Material(_item.StimulusOutlineShader);
            }
        }
    
        /// <summary>
        /// unused function - would control the decay of the stimulus
        /// </summary>
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
            // called when the Dog enters the Stimulus' range for the first time
            // prevents OnTriggerEnter events from previously encountered stimuli
            _trigger.enabled = false;
            StartCoroutine(ShowRange());
        }
    
        
        public void Focused(bool isFocused)
        {
            // called when the dog starts or stops pathfinding to the stimulus
            if (isFocused)
            {
                // the focused stimulus is set to be cyan temporarily
                _focused = true;
                Material material = new(_item.StimulusOutlineShader);
                material.SetColor("_Color", Color.cyan);
                _outline.material = material;
                return;
            }
            _outline.material = _material;
        }
    
        
        public void Inspect()
        {
            // The dog will only inspect each item once
            _item.Inspected = true;
            _item.gameObject.SetActive(false);
        }
        public bool Inspected() => _item.Inspected;
        
        
        private IEnumerator ShowRange()
        {
            // When the dog encounters a stimulus, the line renderer outline is shown.
            // The outline's colour gradually darkens to make encountered and focused stimuli more obvious
            // the red value of the outline is based on the priority of the stimulus
            _material.SetColor("_Color", new Color(Red(), 1, 0.2f, 1));
            _outline.material = _material;
            _outline.enabled = true;
            const int steps = 20;
            yield return new WaitForSeconds(0.5f);
            
            // gradually changes the green value of the colour from 1 to 0.4
            for (float g = 1; g >= 0.4f; g -= 0.6f/steps)
            {
                // if the stimulus becomes focused while it's darkening, the process is paused until the stimulus is no longer focused.
                if (_focused)
                {
                    yield return new WaitUntil(() => !_focused);
                }
                _material.SetColor("_Color", new Color(Red(), g, 0.2f, 1));
                _outline.material = _material;
                
                // process takes a total of 3 seconds
                yield return new WaitForSeconds(3f/steps);
            }
        }
        
        /// <summary>
        /// calculates red value for the outline colour
        /// </summary>
        /// <returns></returns>
        private float Red() => (float)Data.Strength/10;
        
        /// <summary>
        /// Calculates a list of Vector3's representing points of a circle.
        /// </summary>
        /// <param name="r">radius of the circle</param>
        /// <returns></returns>
        private static Vector3[] DrawCircle(int r = 1)
        {
            // step is 0.2 for a unit circle
            float step = r/5f;
            // a unit circle made in steps of 0.2 has 28 segments
            int seg = r * 28;
            
            // points start at (1,0) and go in the anti-clockwise direction
            Vector3[] points = new Vector3[seg];
            float x = 1f;
            float y = 0f;
            
            // calculating the arc of points in quadrant 1 using the equation of a circle
            for (int i = 0; i <= seg/4; i++)
            {
                // when y becomes larger than x, swaps to incrementing the x value
                if (Mathf.Approximately(y, x))
                {
                    x -= step;
                    y = Circle(x);
                }
                // while y is less than x, the y value is incremented
                if (y < x)
                {
                    x = Circle(y);
                    points[i] = new Vector3(x, 0f, y);
                    y += step;
                    
                }
                // while x is less than y, the x value is incremented
                else
                {
                    y = Circle(x);
                    points[i] = new Vector3(x, 0f, y);
                    x -= step;
                }
            }
    
            // calculating quadrant 4: reflects arc in x = 0
            for (int i = (seg/4) + 1; i <= seg/2; i++)
            {
                points[i] = Vector3.Scale(points[(seg/2) - i], new Vector3(-1, 1, 1));
            }
            
            // calculating quadrants 2 and 3: reflects semi-circle in y = 0
            for (int i = (seg/2) + 1; i < seg; i++)
            {
                points[i] = Vector3.Scale(points[seg - i], new Vector3(1, 1, -1));
            }
            
            return points;
    
            // x^2 + y^2 = r^2
            // y = sqrt(r^2 - x^2)
            float Circle(float a) => Mathf.Sqrt((r * r) - (a*a));
        }
    }
}


