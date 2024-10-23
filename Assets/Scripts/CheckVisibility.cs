using UnityEngine;

public class CheckVisibility : MonoBehaviour
{
    private void OnBecameVisible()
    {
        Debug.Log("became visible!");
        if (CameraPlacement.instance.securityCameraView!= null && CameraPlacement.instance.securityCameraView.enabled)
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
