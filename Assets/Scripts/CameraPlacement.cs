// CameraPlacement.cs

using System;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;


// CameraPlacement.cs
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class CameraPlacement : MonoBehaviour
{
    [Header("Camera Settings")]
    public GameObject securityCameraPrefab;
    public float placementRange = 5f;
    public LayerMask wallLayer;
    public Vector2Int renderTextureResolution = new Vector2Int(1920, 1080);

    
    [Header("Input Settings")]
    public KeyCode switchCameraKey = KeyCode.C;
    public KeyCode replaceCameraKey = KeyCode.R;
    
    [Header("Placement Indicator Settings")]
    public GameObject placementIndicatorPrefab;
    public Color validPlacementColor = new Color(0, 1, 0, 0.5f);
    public Color invalidPlacementColor = new Color(1, 0, 0, 0.5f);
    public float indicatorOffset = 0.01f;
    
    public Camera mainCamera;
    private GameObject currentSecurityCamera;
    private bool isViewingSecurityCamera = false;
    public Camera securityCameraView;
    private GameObject placementIndicator;
    private MeshRenderer[] indicatorRenderer;
    private CinemachineBrain mainCameraMovementScript;


    public static CameraPlacement instance;

    private void Awake()
    {
        instance = this;
    }

    void Start(){
        mainCamera = Camera.main;
        mainCamera.enabled = true;
        mainCameraMovementScript = mainCamera.GetComponent<CinemachineBrain>();
        if (mainCameraMovementScript == null)
        {
            Debug.LogWarning("No movement script found on main camera!");
        }
        
        CreatePlacementIndicator();
    }

    void CreatePlacementIndicator()
    {
        placementIndicator = Instantiate(placementIndicatorPrefab);
        indicatorRenderer = placementIndicator.GetComponentsInChildren<MeshRenderer>();
        placementIndicator.SetActive(false);
    }

    void Update()
    {
        // Only show placement indicator when not viewing security camera
        if (!isViewingSecurityCamera)
        {
            UpdatePlacementIndicator();
        }
        else
        {
            placementIndicator.SetActive(false);
        }

        // Place or replace camera
        if (Input.GetMouseButtonDown(0) || (Input.GetKeyDown(replaceCameraKey) && currentSecurityCamera != null))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, placementRange, wallLayer))
            {
                if (currentSecurityCamera != null)
                {
                    if (isViewingSecurityCamera)
                    {
                        SwitchToMainCamera();
                    }
                    Destroy(currentSecurityCamera);
                }
                
                PlaceCamera(hit.point, hit.normal);
            }
        }

        // Switch between cameras
        if (Input.GetKeyDown(switchCameraKey) && currentSecurityCamera != null)
        {
            ToggleCamera();
        }
    }

    void UpdatePlacementIndicator()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, placementRange, wallLayer))
        {
            placementIndicator.SetActive(true);
            
            // Position the indicator slightly offset from the w	all
            Vector3 indicatorPosition = hit.point + (hit.normal * indicatorOffset);
            placementIndicator.transform.position = indicatorPosition;
            
            // Rotate indicator to match wall normal
            placementIndicator.transform.rotation = Quaternion.LookRotation(hit.normal);
            
            // Check if placement is valid (you can add more conditions here)
            bool isValidPlacement = true; // Add your validation logic here
            
            // Update indicator color
            foreach (var renderer in indicatorRenderer)
            {
                Material indicatorMaterial = renderer.material;
                indicatorMaterial.color = isValidPlacement ? validPlacementColor : invalidPlacementColor;
            }
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    void PlaceCamera(Vector3 position, Vector3 normal)
    {
        Vector3 offsetPosition = position + (normal * 0.1f);
        currentSecurityCamera = Instantiate(securityCameraPrefab, offsetPosition, Quaternion.identity);
        currentSecurityCamera.transform.rotation = Quaternion.LookRotation(normal);
        
        securityCameraView = currentSecurityCamera.GetComponentInChildren<Camera>();
        if (securityCameraView == null)
        {
            Debug.LogError("Security camera prefab must have a Camera component!");
            return;
        }

        
        securityCameraView.enabled = false;
        currentSecurityCamera.AddComponent<WallCamera>();
    }


    void ToggleCamera()
    {
        if (isViewingSecurityCamera)
        {
            SwitchToMainCamera();
        }
        else
        {
            SwitchToSecurityCamera();
        }
    }

    void SwitchToMainCamera()
    {
        mainCamera.enabled = true;
        mainCameraMovementScript.enabled = true;
        if (securityCameraView != null)
        {
            securityCameraView.enabled = false;
        }
        isViewingSecurityCamera = false;
    }

    void SwitchToSecurityCamera()
    {
        mainCamera.enabled = false;
        mainCameraMovementScript.enabled = false;
        securityCameraView.enabled = true;
        isViewingSecurityCamera = true;
    }


}