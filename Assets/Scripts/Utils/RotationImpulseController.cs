using UnityEngine;

public class RotationImpulseController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    private Vector3 rotationImpulse = new Vector3(0, 10, 0);
    [SerializeField]
    private float damping = 1f;
    [SerializeField]
    private bool impulseOnStart;
    private Vector3 currentAngularVelocity;

    private void Start()
    {
        if (impulseOnStart) ApplyTorque();
    }

    void Update()
    {
        transform.Rotate(currentAngularVelocity * Time.deltaTime);
        currentAngularVelocity = Vector3.Lerp(currentAngularVelocity, Vector3.zero, damping * Time.deltaTime);
    }

    public void ApplyTorque(Vector3 impulse)
    {
        currentAngularVelocity += impulse;
    }

    [EasyButtons.Button]
    public void ApplyTorque()
    {
        ApplyTorque(rotationImpulse);
    }

    public void Stop()
    {
        currentAngularVelocity = Vector3.zero;
    }
}
