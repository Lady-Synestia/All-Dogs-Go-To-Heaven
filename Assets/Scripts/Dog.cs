using Events.DogEvents;
using States;
using UnityEngine;
using UnityEngine.AI;

public class Dog : MonoBehaviour
{
    
    private StateMachine _stateMachine;
    private DogEventObserver _eventObserver;
    private const int TimeToWait = 0;
    private float _timeElapsed;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _stateMachine = new StateMachine(GetComponent<NavMeshAgent>());
        _eventObserver = gameObject.AddComponent<DogEventObserver>();
        _eventObserver.OnEvent += EventRaised;
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
        }
    }

    private void StimulusEncountered(Stimulus stimulus)
    {
        stimulus.Encountered();
        float distance = Vector3.Distance(transform.position, stimulus.transform.position);
        int priority = Mathf.RoundToInt(stimulus.Data.Strength/ distance);
        Debug.Log($"Item: {stimulus.transform.parent.name}. Stimulus: {stimulus.Data.Sense}. Priority: {priority}. Strength: {stimulus.Data.Strength}. Distance: {distance}.");
        _stateMachine.AddToQueue(stimulus, priority);
        
    }
}
