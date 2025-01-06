using UnityEngine;
using UnityEngine.AI;

namespace States
{
    public class StateMachine
    {
        protected State CurrentState;
        
        public void Update()
        {
            CurrentState.Execute();
        }
    }

    public class DogStateMachine : StateMachine
    {
        private NavMeshAgent _agent;
        
        public DogStateMachine(NavMeshAgent agent)
        {
            _agent = agent;
            CurrentState = new SeekState(agent, Vector3.zero);
        }
    }
}
