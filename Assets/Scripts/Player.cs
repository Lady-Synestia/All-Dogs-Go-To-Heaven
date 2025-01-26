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
    
    private void Start()
    {
        _movementEventObserver = gameObject.AddComponent<PlayerMovementObserver>();
        _movementEventObserver.Subscribe(MovementEventRaised);
    }
    
    private void FixedUpdate()
    {
        // the real speed values account for changes in frame rate
        _movementSpeed = movementSpeed * Time.fixedDeltaTime;
        _rotationSpeed = rotationSpeed * Time.fixedDeltaTime;
    }

    private void MovementEventRaised(object caller, PlayerEvent e)
    {
        switch (e.EventType)
        {
            case PlayerEvent.Type.Move:
                MovementEventArgs moveArgs = (MovementEventArgs)e.Args;
                // camera movement is based on rotation
                gameObject.transform.position += CalculateMovement(moveArgs);
                break;
            
            case PlayerEvent.Type.Look:
                var lookArgs = (MovementEventArgs)e.Args;
                Quaternion rotation = transform.rotation;
                // input values for the rotation are based on the movement of the cursor
                rotation.eulerAngles += lookArgs.Value * _rotationSpeed;
                gameObject.transform.rotation = rotation;
                break;
        }
    }

    /// <summary>
    /// Calculates the real movement of the camera based on its rotation.
    /// </summary>
    /// <param name="moveArgs">Raw input values</param>
    /// <returns>Rotated values</returns>
    private Vector3 CalculateMovement(MovementEventArgs moveArgs)
    { 
        // vertical axis is not rotated
        Vector3 rotatedValues = new (0f, moveArgs.Value.y, 0f);
        Vector2 direction2D = new (-moveArgs.Value.x, moveArgs.Value.z);

        // scalar for quadrant 1 is (1,1,1)
        Vector3 scalar = Vector3.one;
        
        if (direction2D != Vector2.zero)
        {
            // calculating angle of movement around the y-axis
            float theta = transform.eulerAngles.y + Vector2.SignedAngle(Vector2.up, direction2D);
                    
            // calculating z and x values based on angle of movement using basic trig
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
        
        return Vector3.Scale(rotatedValues, scalar) * _movementSpeed;
    }
    
    /// <summary>
    /// Uses opposite = h*sin(theta) and adjacent = h*cos(theta) to calculate
    /// rotated movement values.
    /// </summary>
    private static (float o, float a) RotateMovement(float alpha, int h = 1)
    {
        alpha *= Mathf.Deg2Rad;
        // h defaults to 1 as the input values are 1 or 0
        float o = h * Mathf.Sin(alpha);
        float a = h * Mathf.Cos(alpha);
        return (o, a);
    }
}
