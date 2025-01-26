using Events.PlayerEvents;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    [Header("Movement Settings")]
    [SerializeField]
    [Range(1, 10)]
    private int movementSpeed = 5;
    private float _movementSpeed;
    [SerializeField]
    [Range(1, 90)]
    private float rotationSpeed = 30f;
    private float _rotationSpeed;
    
    private PlayerMovementObserver _movementEventObserver;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _movementEventObserver = gameObject.AddComponent<PlayerMovementObserver>();
        _movementEventObserver.Subscribe(MovementEventRaised);
    }
    private void FixedUpdate()
    {
        _movementSpeed = movementSpeed * Time.fixedDeltaTime;
        _rotationSpeed = rotationSpeed * Time.fixedDeltaTime;
    }

    private void MovementEventRaised(object caller, PlayerEvent e)
    {
        switch (e.EventType)
        {
            case PlayerEvent.Type.Move:
                MovementEventArgs moveArgs = (MovementEventArgs)e.Args;
                Vector3 rotatedValues = new Vector3(0f, moveArgs.Value.y, 0f);
                Vector2 direction2D = new Vector2(-moveArgs.Value.x, moveArgs.Value.z);

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
                var lookArgs = (MovementEventArgs)e.Args;
                Quaternion rotation = transform.rotation;
                rotation.eulerAngles += lookArgs.Value * _rotationSpeed;
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
