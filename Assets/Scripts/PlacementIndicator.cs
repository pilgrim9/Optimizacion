// Create a new script for the placement indicator
// PlacementIndicator.cs
using UnityEngine;

public class PlacementIndicator : MonoBehaviour
{
    public float pulseSpeed = 1f;
    public float pulseMinScale = 0.9f;
    public float pulseMaxScale = 1.1f;
    
    private Vector3 originalScale;
    private float pulseTime;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        // Create a subtle pulse effect
        pulseTime += Time.deltaTime * pulseSpeed;
        float pulseFactor = Mathf.Lerp(pulseMinScale, pulseMaxScale, 
            (Mathf.Sin(pulseTime) + 1f) * 0.5f);
        
        transform.localScale = originalScale * pulseFactor;
    }
}