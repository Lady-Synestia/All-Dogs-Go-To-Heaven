using Events.DogEvents;
using UnityEngine.AI;

namespace States
{
    public class StateMachine
    {
        public State CurrentState;
        public readonly NavMeshAgent Agent;
        public readonly DogEventObserver DogEventObserver;
        public BucketQueue<Stimulus> Queue = new();
        
        public StateMachine(NavMeshAgent agent, DogEventObserver dogEventObserver)
        {
            Agent = agent;
            DogEventObserver = dogEventObserver;
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
