using UnityEngine;
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