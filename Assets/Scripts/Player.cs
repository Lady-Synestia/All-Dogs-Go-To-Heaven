using Events;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    [Header("Movement Settings")]
    [SerializeField]
    [Range(1, 10)]
    private int movementSpeed = 5;
    private float _movementSpeed;
    [SerializeField]
    private float rotationSpeed = 0.1f;
    
    private PlayerMovementObserver _movementEventObserver;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _movementEventObserver = gameObject.AddComponent<PlayerMovementObserver>();
        _movementEventObserver.OnEvent += MovementEventRaised;
        
        _movementSpeed = movementSpeed * 0.005f;
    }
    private void Update()
    {
    }

    private void MovementEventRaised(object caller, PlayerEvent e)
    {
        switch (e.EventType)
        {
            case PlayerEvent.Type.Move:
                var moveEvent = (MovementEvent)e;
                Vector3 rotatedValues = new Vector3(0f, moveEvent.Value.y, 0f);
                Vector2 direction2D = new Vector2(-moveEvent.Value.x, moveEvent.Value.z);

                Vector3 scalar = Vector3.one;
                if (direction2D != Vector2.zero)
                {
                    // calculating angle of movement
                    float theta = transform.eulerAngles.y + Vector2.SignedAngle(Vector2.up, direction2D);
                    
                    // calculating z and x values based on angle of movement
                    // calculation changes based on the quadrant the angle is in
                    switch (theta)
                    {
                        case > 270:
                            // quadrant 4
                            // z = opp, x = -adj
                            (rotatedValues.z, rotatedValues.x) = RotateMovement(theta - 270);
                            scalar = new Vector3(-1f, 1f, 1f);
                            break;
                        case > 180:
                            // quadrant 3
                            // x = -opp, z = -adj
                            (rotatedValues.x, rotatedValues.z) = RotateMovement(theta - 180);
                            scalar = new Vector3(-1f, 1f, -1f);
                            break;
                        case > 90:
                            // quadrant 2
                            // z = -opp, x = adj
                            (rotatedValues.z, rotatedValues.x) = RotateMovement(theta - 90);
                            scalar = new Vector3(1f, 1f, -1f);
                            break;
                        default:
                            // quadrant 1
                            // x = opp, z = adj
                            (rotatedValues.x, rotatedValues.z) = RotateMovement(theta);
                            break;
                    }
                }
                
                
                gameObject.transform.position += Vector3.Scale(rotatedValues, scalar) * _movementSpeed;
                break;
            
            case PlayerEvent.Type.Look:
                var lookEvent = (MovementEvent)e;
                Quaternion rotation = transform.rotation;
                rotation.eulerAngles += lookEvent.Value * rotationSpeed;
                gameObject.transform.rotation = rotation;
                break;
        }
    }
    private static (float o, float a) RotateMovement(float alpha)
    {
        alpha *= Mathf.Deg2Rad;
        float o = Mathf.Sin(alpha);
        float a = Mathf.Cos(alpha);
        return (o, a);
    }
}
