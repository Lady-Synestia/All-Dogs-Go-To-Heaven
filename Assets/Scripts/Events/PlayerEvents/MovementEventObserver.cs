using UnityEngine;
using System;
using UnityEngine.InputSystem;

namespace Events.PlayerEvents
{
    public class PlayerMovementObserver: PlayerEventObserver
    {
        
        private InputAction _moveAction;
        private InputAction _lookAction;
        private InputAction _mousePosition;

        private void Awake()
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