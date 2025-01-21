using Events.DogEvents;
using States;
using UnityEngine;
using UnityEngine.AI;

public class Dog : MonoBehaviour
{
    
    private StateMachine _stateMachine;
    private DogEventObserver _eventObserver;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _stateMachine = new StateMachine(GetComponent<NavMeshAgent>());
        _eventObserver = gameObject.AddComponent<DogEventObserver>();
        _eventObserver.OnEvent += EventRaised;
    }

    private void Update()
    {
        _stateMachine.Update();
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
        Debug.Log($"Stimulus encountered: {stimulus.Data.Sense}, Strength: {stimulus.Data.Strength}");
        _stateMachine.SetTarget(stimulus.transform.parent);
    }
}
