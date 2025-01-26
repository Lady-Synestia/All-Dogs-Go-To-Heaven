using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Events.PlayerEvents
{
    /// <summary>
    /// Watches for User Inputs, and will call appropriate Player Movement Events based on input.
    /// </summary>
    public class PlayerMovementObserver: EventObserver<PlayerEvent>
    {
        /// <summary>
        /// Input action for moving the camera -> WASD and QE
        /// </summary>
        private InputAction _moveAction;
        
        /// <summary>
        /// Input action for enabling look rotation -> right mouse button
        /// </summary>
        private InputAction _lookAction;
        
        /// <summary>
        /// Input action for direction of look rotation -> cursor position
        /// </summary>
        private InputAction _mousePosition;

        private void Awake()
        {
            _moveAction = InputSystem.actions.FindAction("Move");
            _lookAction = InputSystem.actions.FindAction("LookEnable");
            _mousePosition = InputSystem.actions.FindAction("Look");
        }

        private void FixedUpdate()
        {
            if (_moveAction.IsPressed())
            {
                // uses raw input values for the event arguments, these are used for calculation later.
                MovementEventArgs args = new ()
                {
                    Value = _moveAction.ReadValue<Vector3>()
                };
                
                RaiseEvent(new PlayerEvent(PlayerEvent.Type.Move, args));
            }

            if (_lookAction.IsPressed())
            {
                Vector2 look2D = _mousePosition.ReadValue<Vector2>();
                if (look2D != Vector2.zero)
                {
                    // converts the 2D input into a correct 3D Vector.
                    // swaps direction of movement to the axis the rotation will happen around, e.g. vertical mouse movement = rotation around the x-axis.
                    MovementEventArgs args = new()
                    {
                        Value = new Vector3(-look2D.y, look2D.x, 0f)
                    };
                    
                    RaiseEvent(new PlayerEvent(PlayerEvent.Type.Look, args));
                }
            }
        }
    }
}