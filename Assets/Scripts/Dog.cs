using System;
using Events.DogEvents;
using States;
using UnityEngine;
using UnityEngine.AI;

public class Dog : MonoBehaviour
{
    
    private StateMachine _stateMachine;
    private StimulusObserver _stimulusObserver;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _stateMachine = new StateMachine(GetComponent<NavMeshAgent>());
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
        switch (stimulus.Sense)
        {
            case Stimulus.SenseType.Visual:
                Debug.Log($"Visual Stimulus Encountered. Strength: {stimulus.Strength}");
                break;
            case Stimulus.SenseType.Auditory:
                Debug.Log($"Auditory Stimulus Encountered. Strength: {stimulus.Strength}");
                break;
            case Stimulus.SenseType.Olfactory:
                Debug.Log($"Olfactory Stimulus Encountered. Strength: {stimulus.Strength}");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
