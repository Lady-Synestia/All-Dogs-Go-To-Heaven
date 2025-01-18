using Events;
using States;
using UnityEngine;
using UnityEngine.AI;

public class Dog : MonoBehaviour
{
    
    private DogStateMachine _stateMachine;
    private StimulusObserver _stimulusObserver;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _stateMachine = new DogStateMachine(GetComponent<NavMeshAgent>());
        _stimulusObserver = gameObject.AddComponent<StimulusObserver>();
        _stimulusObserver.OnEvent += StimulusEncountered;
    }

    private void Update()
    {
        _stateMachine.Update();
    }

    private void StimulusEncountered(object sender, DogEvent e)
    {
        var stimulusEvent = (StimulusEvent)e;
        Stimulus stimulus = stimulusEvent.Stimulus;
        Debug.Log($"Stimulus Encountered: {stimulus.Sense}, strength: {stimulus.Strength}");
    }
}
