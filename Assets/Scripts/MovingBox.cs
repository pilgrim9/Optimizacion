using UnityEngine;

public class MovingBox : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;        // Speed of movement
    public float moveDistance = 5f;     // Distance to move from center (in each direction)
    public bool startMovingRight = true; // Initial direction
    
    private Vector3 startPosition;
    private float leftBound;
    private float rightBound;
    private bool movingRight;

    void Start()
    {
        // Store the starting position
        startPosition = transform.position;
        
        // Calculate boundaries
        leftBound = startPosition.x - moveDistance;
        rightBound = startPosition.x + moveDistance;
        
        // Set initial direction
        movingRight = startMovingRight;
    }

    void Update()
    {
        // Get current position
        float currentX = transform.position.x;
        
        // Check if we need to change direction
        if (currentX >= rightBound)
        {
            movingRight = false;
        }
        else if (currentX <= leftBound)
        {
            movingRight = true;
        }
        
        // Calculate movement
        float movement = moveSpeed * Time.deltaTime * (movingRight ? 1 : -1);
        
        // Apply movement
        transform.Translate(new Vector3(movement, 0, 0));
    }

    // Optional: Visualize the movement bounds in the editor
    void OnDrawGizmosSelected()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(new Vector3(leftBound, transform.position.y, transform.position.z), 0.5f);
            Gizmos.DrawWireSphere(new Vector3(rightBound, transform.position.y, transform.position.z), 0.5f);
        }
    }
}