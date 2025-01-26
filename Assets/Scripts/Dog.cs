using Events.DogEvents;
using States;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class Dog : MonoBehaviour
{
    
    public DogEventObserver EventObserver { get; private set; }
    public int ItemsInspected { get; private set; }
    private StateMachine _stateMachine;
    private const int TimeToWait = 0;
    private float _timeElapsed;
    
    private void Awake()
    {
        EventObserver = gameObject.AddComponent<DogEventObserver>();
        EventObserver.Subscribe(EventRaised);
        _stateMachine = new StateMachine(GetComponent<NavMeshAgent>(), EventObserver);
        _timeElapsed = TimeToWait;
    }

    private void Update()
    {
        _timeElapsed += Time.deltaTime;
        if (_timeElapsed >= TimeToWait)
        {
            _timeElapsed = 0;
            _stateMachine.Update();
        }
    }

    private void EventRaised(object sender, DogEvent e)
    {
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
        float distance = Vector3.Distance(transform.position, stimulus.transform.position);
        int priority = Mathf.RoundToInt(stimulus.Data.Strength/ distance);
        // Debug.Log($"Item: {stimulus.transform.parent.name}. Stimulus: {stimulus.Data.Sense}. Priority: {priority}. Strength: {stimulus.Data.Strength}. Distance: {distance}.");
        _stateMachine.AddToQueue(stimulus, priority);
    }
}
