using System;
using Events.UIEvents;
using UnityEngine;

namespace States
{
    /// <summary>
    /// Initial state, and the state when there are no stimuli in the queue
    /// </summary>
    public class IdleState : State
    {
        private const float Timer = 2.0f;
        private float _startTime;
        
        public override void Execute()
        {
            if (!StateMachine.Queue.IsEmpty())
            {
                // transitions to seek state if there is anything in the queue
                StateMachine.CurrentState = new SeekState(StateMachine, StateMachine.Queue.Peek());
            }
            else
            {
                // if nothing is in the queue for 2 seconds, the game ends.
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