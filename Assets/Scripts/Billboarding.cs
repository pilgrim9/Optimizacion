using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboarding : MonoBehaviour
{
    Transform mainCamera;
    private void Start()
    {
        mainCamera = Camera.main.gameObject.transform;
    }

    
    void Update()
    {
        transform.LookAt(mainCamera);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }
}
