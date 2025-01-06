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
                
                float theta = (transform.eulerAngles.y) * Mathf.Deg2Rad;
                float aSinTheta = Mathf.Asin(theta);
                float aCosTheta = Mathf.Acos(theta);
                float x = rawValues.x;
                float z = rawValues.z;
                rotatedValues.x = (x * aCosTheta) + (z  * aSinTheta);
                rotatedValues.z = (x * aSinTheta) + (z * aCosTheta);
                
                gameObject.transform.position += rotatedValues * movementSpeed;
                break;
            
            case PlayerEvent.Type.Look:
                var lookEvent = (MovementEvent)e;
                Quaternion rotation = transform.rotation;
                rotation.eulerAngles += lookEvent.Value * rotationSpeed;
                gameObject.transform.rotation = rotation;
                break;
        }
    }
}
