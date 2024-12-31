using States;
using UnityEngine;
using UnityEngine.AI;

public class Dog : MonoBehaviour
{
    private NavMeshAgent _agent;
    
    private NavStateMachine _stateMachine;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _stateMachine = new NavStateMachine(_agent);
    }

    // Update is called once per frame
    private void Update()
    {
        _stateMachine.Request();
    }
}
