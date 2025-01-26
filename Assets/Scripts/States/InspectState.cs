using System;
using Events.DogEvents;
using Items;
using UnityEngine;

namespace States
{
    /// <summary>
    /// State dog is in while inspecting an item
    /// </summary>
    public class InspectState : State
    {
        private const float Timer = 1.0f;
        private float _startTime;
        private Stimulus _stimulus;
        
        public override void Execute()
        {
            // Dog spends 1 second inspecting each item
            float timeElapsed = Time.time - _startTime;
            if (timeElapsed >= Timer)
            {
                _stimulus.Inspect();
                // notifies the dog that an item has been inspected so it can increment its counter
                StateMachine.DogEventObserver.RaiseEvent(new DogEvent(DogEvent.Type.ItemInspected, EventArgs.Empty));
                
                // always transitions back to the idle state
                StateMachine.CurrentState = new IdleState(StateMachine);
            }
        }

        public InspectState(StateMachine stateMachine, Stimulus stimulus) : base(stateMachine)
        {
            _stimulus = stimulus;
            _startTime = Time.time;
        }
    }
}