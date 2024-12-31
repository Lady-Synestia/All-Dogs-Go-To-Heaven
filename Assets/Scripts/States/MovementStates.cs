using UnityEngine.AI;
using UnityEngine;

namespace States
{
    public abstract class NavState : State
    {
        protected NavMeshAgent Agent;

        protected NavState(NavMeshAgent agent)
        {
            this.Agent = agent;
        }
    }

    public class SeekState : NavState
    {
        private Vector3 _target;
        
        public override void Execute()
        {  
            Debug.Log("Seek");
        }

        public SeekState(NavMeshAgent agent, Vector3 target) : base(agent)
        {
            _target = target;
        }
    }
}