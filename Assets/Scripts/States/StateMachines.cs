using UnityEngine;
using UnityEngine.AI;

namespace States
{
    public class StateMachine
    {
        protected State CurrentState;
        
        public void Request()
        {
            CurrentState.Execute();
        }
    }

    public class NavStateMachine : StateMachine
    {
        private NavMeshAgent _agent;
        public NavStateMachine(NavMeshAgent agent)
        {
            _agent = agent;
            CurrentState = new SeekState(agent, Vector3.zero);
        }
    }
}
