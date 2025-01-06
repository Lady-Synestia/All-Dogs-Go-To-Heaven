using States;
using UnityEngine;
using UnityEngine.AI;
using States;

public class Dog : MonoBehaviour
{
    
    private DogStateMachine _stateMachine;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _stateMachine = new DogStateMachine(GetComponent<NavMeshAgent>());
    }

    private void Update()
    {
        _stateMachine.Update();
    }
}
