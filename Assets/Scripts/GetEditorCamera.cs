
using UnityEditor;
using UnityEngine;

public class GetEditorCamera : MonoBehaviour
{
    [SerializeField]
    Transform[] cameraPositions;

    [SerializeField]
    public RenderTexture sceneCaptureTexture;

    Camera editorCamera;

    int currentPositionIndex = 0;

    void Start()
    {
        editorCamera = SceneView.lastActiveSceneView.camera;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentPositionIndex--;
            if (currentPositionIndex < 0)
            {
                currentPositionIndex = cameraPositions.Length - 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            currentPositionIndex++;
            if (currentPositionIndex >= cameraPositions.Length)
            {
                currentPositionIndex = 0;
            }
        }

        if (cameraPositions.Length > 0)
        {
            editorCamera.transform.position = cameraPositions[currentPositionIndex].position;
            editorCamera.transform.rotation = cameraPositions[currentPositionIndex].rotation;
        }

        RenderTexture.active = sceneCaptureTexture;
        editorCamera.Render();
    }
}
