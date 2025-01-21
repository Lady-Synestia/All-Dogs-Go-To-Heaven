using UnityEngine;
using UnityEngine.AI;

namespace States
{
    public class StateMachine
    {
        internal State CurrentState;
        internal readonly NavMeshAgent Agent;
        internal BucketQueue<Stimulus> Queue = new();
        
        public StateMachine(NavMeshAgent agent)
        {
            Agent = agent;
            CurrentState = new IdleState(this);
        }
        
        public void Update()
        {
            CurrentState.Execute();
        }

        public void AddToQueue(Stimulus stimulus, int priority)
        {
            if (Queue.Insert(priority, stimulus))
            {
                CurrentState.Execute();
            }
        }
    }
}
