using UnityEngine.AI;
using UnityEngine;

namespace States
{
    public class SeekState : State
    {
        private Vector3 _target;
        protected NavMeshAgent Agent;
        
        public override void Execute()
        {  
            // Debug.Log("Seek");
        }

        public SeekState(NavMeshAgent agent, Vector3 target)
        {
            this.Agent = agent;
            _target = target;
        }
    }
}