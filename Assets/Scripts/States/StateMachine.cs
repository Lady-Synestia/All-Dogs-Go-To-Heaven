using Events.DogEvents;
using Items;
using UnityEngine.AI;

namespace States
{
    /// <summary>
    /// The Dog's state machine - controls the dog's actions
    /// </summary>
    public class StateMachine
    {
        public State CurrentState;
        public NavMeshAgent Agent { get; private set; }
        
        // used by the idle state to call the ItemInspected event
        public DogEventObserver DogEventObserver { get; private set; }
        
        // Priority queue storing the encountered stimuli
        public BucketQueue<Stimulus> Queue { get; } = new(BucketQueue.OrderType.Lifo);
        
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

        /// <summary>
        /// Adds a stimulus to the priority queue. Stimuli are visited highest priority first.
        /// </summary>
        /// <param name="stimulus">Stimulus to be added</param>
        /// <param name="priority">priority of the stimulus</param>
        public void AddToQueue(Stimulus stimulus, int priority)
        {
            if (Queue.Insert(priority, stimulus))
            {
                // updates the current state if the stimulus added was the new highest priority
                CurrentState.Execute();
            }
        }
    }
}
