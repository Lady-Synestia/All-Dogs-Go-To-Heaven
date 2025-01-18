using UnityEngine;
using UnityEngine.AI;

namespace States
{
    public class StateMachine
    {
        private State _currentState;
        
        private NavMeshAgent _agent;

        public StateMachine(NavMeshAgent agent)
        {
            _agent = agent;
            _currentState = new SeekState(agent, Vector3.zero);
        }
        
        public void Update()
        {
            _currentState.Execute();
        }
    }
}
