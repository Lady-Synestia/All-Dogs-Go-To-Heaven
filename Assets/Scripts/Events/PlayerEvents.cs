using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Events
{
    public class PlayerEvent : GameEvent
    {
        public enum Type
        {
            Move,
            Look,
        }

        public PlayerEvent(Type e) : base(e) { }
    }

    public class MovementEvent : PlayerEvent
    {
        public Vector3 Value { get; private set; }

        public MovementEvent(Vector3 value, Type e) : base(e) { Value = value; }
    }

    public class PlayerEventObserver : MonoBehaviour, IEventObserver<PlayerEvent>
    {
        public event EventHandler<PlayerEvent> OnEvent;
        
        public void RaiseEvent(PlayerEvent e)
        {
            OnEvent?.Invoke(this, e);
        }
    }

    public class PlayerMovementObserver: PlayerEventObserver
    {
        
        private InputAction _moveAction;
        private InputAction _lookAction;
        private InputAction _mousePosition;

        private void Start()
        {
            _moveAction = InputSystem.actions.FindAction("Move");
            _lookAction = InputSystem.actions.FindAction("LookEnable");
            _mousePosition = InputSystem.actions.FindAction("Look");
        }

        private void Update()
        {
            if (_moveAction.IsPressed())
            {
                RaiseEvent(new MovementEvent(_moveAction.ReadValue<Vector3>(), PlayerEvent.Type.Move));
            }

            if (_lookAction.IsPressed())
            {
                Vector2 look2D = _mousePosition.ReadValue<Vector2>();
                if (look2D != Vector2.zero)
                {
                    var look3D = new Vector3(look2D.y, look2D.x, 0f);
                    RaiseEvent(new MovementEvent(look3D, PlayerEvent.Type.Look));
                }
            }
        }
    }
}