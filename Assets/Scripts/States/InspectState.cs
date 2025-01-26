using System;
using Events.DogEvents;
using UnityEngine;

namespace States
{
    public class InspectState : State
    {
        private const float Timer = 1.0f;
        private float _startTime;
        private Stimulus _stimulus;
        
        public override void Execute()
        {
            float timeElapsed = Time.time - _startTime;
            if (timeElapsed >= Timer)
            {
                _stimulus.Inspect();
                StateMachine.DogEventObserver.RaiseEvent(new DogEvent(DogEvent.Type.ItemInspected, EventArgs.Empty));
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