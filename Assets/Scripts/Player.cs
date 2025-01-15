using Events;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    [Header("Movement Settings")]
    public float movementSpeed = 0.1f;
    public float rotationSpeed = 0.1f;
    
    private PlayerEventObserver _eventObserver;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _eventObserver = gameObject.AddComponent<PlayerEventObserver>();
        _eventObserver.OnEvent += EventRaised;
    }
    private void Update()
    {
    }

    private void EventRaised(object caller, PlayerEvent e)
    {
        switch (e.EventType)
        {
            case PlayerEvent.Type.Move:
                var moveEvent = (MovementEvent)e;
                Vector3 rawValues = moveEvent.Value;
                Vector3 rotatedValues = new Vector3(0f, rawValues.y, 0f);

                float theta = transform.eulerAngles.y;
                float alpha;
                Vector3 scalar;
                switch (theta)
                {
                    case > 270:
                        scalar = new Vector3(-1f, 1f, 1f);
                        alpha = theta - 270;
                        break;
                    case > 180:
                        scalar = new Vector3(-1f, 1f, -1f);
                        alpha = theta - 180;
                        break;
                    case > 90:
                        scalar = new Vector3(1f, 1f, -1f);
                        alpha = theta - 90;
                        break;
                    default:
                        scalar = new Vector3(1f, 1f, 1f);
                        alpha = theta;
                        break;
                }

                if (alpha > 45)
                {
                    alpha = 90 - alpha;
                    (rotatedValues.z, rotatedValues.x) = RotateMovement(rawValues.z, rawValues.x, alpha);
                }
                else
                {
                    (rotatedValues.x, rotatedValues.z) = RotateMovement(rawValues.x, rawValues.z, alpha);
                }
                
                
                gameObject.transform.position += Vector3.Scale(rotatedValues, scalar) * movementSpeed;
                break;
            
            case PlayerEvent.Type.Look:
                var lookEvent = (MovementEvent)e;
                Quaternion rotation = transform.rotation;
                rotation.eulerAngles += lookEvent.Value * rotationSpeed;
                gameObject.transform.rotation = rotation;
                break;
        }
    }

    private (float opp, float adj) RotateMovement(float opp, float adj, float alpha)
    {
        alpha *= Mathf.Deg2Rad;
        float aSinAlpha = Mathf.Asin(alpha);
        float aCosAlpha = Mathf.Acos(alpha);
        float oppOut = (opp * aCosAlpha) + (adj  * aSinAlpha);
        float adjOut = (adj * aSinAlpha) + (opp * aCosAlpha);
        return (oppOut, adjOut);
    }
}
