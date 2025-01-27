using Events.DogEvents;
using Items;
using States;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Controlling class for the Dog
/// </summary>
public class Dog : MonoBehaviour
{
    public DogEventObserver EventObserver { get; private set; }
    public int ItemsInspected { get; private set; }
    private StateMachine _stateMachine;
    
    // controls the time between state machine updates
    private const int TimeToWait = 0;
    private float _timeElapsed;
    
    private void Awake()
    {
        EventObserver = gameObject.AddComponent<DogEventObserver>();
        EventObserver.Subscribe(EventRaised);
        _stateMachine = new StateMachine(GetComponent<NavMeshAgent>(), EventObserver);
        
        // forces a state machine update on the first frame
        _timeElapsed = TimeToWait;
    }

    private void FixedUpdate()
    {
        // updates the state machine at a set interval
        _timeElapsed += Time.deltaTime;
        if (_timeElapsed >= TimeToWait)
        {
            _timeElapsed = 0;
            _stateMachine.Update();
        }
    }

    private void EventRaised(object sender, DogEvent e)
    {
        // function is subscribed to the OnEvent handler of the DogEventObserver
        switch (e.EventType)
        {
            case DogEvent.Type.Stimulus:
                var stimulusArgs = (StimulusEventArgs)e.Args;
                StimulusEncountered(stimulusArgs.Stimulus);
                break;
            case DogEvent.Type.ItemInspected:
                ItemsInspected++;
                break;
        }
    }

    private void StimulusEncountered(Stimulus stimulus)
    {
        stimulus.Encountered();
        
        // calculating the priority of the stimulus and adding it to the priority queue
        float distance = Vector3.Distance(transform.position, stimulus.transform.position);
        int priority = CalculatePriority(stimulus.Data, distance);
        _stateMachine.AddToQueue(stimulus, priority);
        // Debug.Log($"Encountered Stimulus: {stimulus.Data.Strength} strength, distance: {distance} priority: {priority}");
    }

    private static int CalculatePriority(StimulusData data, float distance)
    {
        // priority is based on the strength, type, and distance from the stimulus
        int interest = data.Strength * ((int)data.Sense + 1);
        int priority = Mathf.RoundToInt(interest/ distance);
        return priority;
    }
}
