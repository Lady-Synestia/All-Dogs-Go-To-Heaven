using System;
using Events.UIEvents;
using UnityEngine;

namespace States
{
    public class IdleState : State
    {
        private const float Timer = 2.0f;
        private float _startTime;
        
        public override void Execute()
        {
            if (!StateMachine.Queue.IsEmpty())
            {
                StateMachine.CurrentState = new SeekState(StateMachine);
            }
            else
            {
                float timeElapsed = Time.time - _startTime;
                if (timeElapsed >= Timer)
                {
                    UIEventObserver.Instance.RaiseEvent(new UIEvent(UIEvent.Type.GameEnd, EventArgs.Empty));
                }
            }
        }

        public IdleState(StateMachine stateMachine) : base(stateMachine)
        {
            _startTime = Time.time;
        }
    }
}