using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    private Transform mainCam;
    private void Start()
    {
        mainCam = Camera.main.transform;
    }

    private void Update()
    {
        transform.LookAt(mainCam);
    }
}
