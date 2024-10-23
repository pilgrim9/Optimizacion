using UnityEngine;

public class WallCamera : MonoBehaviour
{
    [Header("Camera Properties")]
    public float rotationSpeed = 30f;
    public float maxRotationAngle = 60f;
    
    private float currentRotation = 0f;
    private bool rotatingRight = true;
    private Vector3 originalForward;
    private Quaternion baseRotation;

    void Start()
    {
        // Store the initial forward direction and rotation
        originalForward = transform.forward;
        baseRotation = transform.rotation;
    }

    // void Update()
    // {
    //     // Calculate the new rotation
    //     float rotationAmount = rotationSpeed * Time.deltaTime * (rotatingRight ? 1 : -1);
    //     currentRotation += rotationAmount;

    //     // Check if we need to change direction
    //     if (Mathf.Abs(currentRotation) >= maxRotationAngle)
    //     {
    //         rotatingRight = !rotatingRight;
    //         currentRotation = Mathf.Sign(currentRotation) * maxRotationAngle;
    //     }

    //     // Apply the rotation around the forward axis of the wall
    //     transform.rotation = baseRotation * Quaternion.Euler(0f, currentRotation, 0f);
    // }

    // Optional: Add methods for controlling the camera's behavior
    public void SetRotationSpeed(float speed)
    {
        rotationSpeed = speed;
    }

    public void SetMaxRotationAngle(float angle)
    {
        maxRotationAngle = angle;
    }
}