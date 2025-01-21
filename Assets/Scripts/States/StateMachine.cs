using UnityEngine;
using UnityEngine.AI;

namespace States
{
    public class StateMachine
    {
        internal State CurrentState;
        internal readonly NavMeshAgent Agent;
        internal bool HasTarget = false;
        internal Transform Target;
        
        public StateMachine(NavMeshAgent agent)
        {
            Agent = agent;
            CurrentState = new IdleState(this);
        }
        
        public void Update()
        {
            CurrentState.Execute();
        }

        public void SetTarget(Transform target)
        {
            Target = target;
            HasTarget = true;
        }
    }
}
