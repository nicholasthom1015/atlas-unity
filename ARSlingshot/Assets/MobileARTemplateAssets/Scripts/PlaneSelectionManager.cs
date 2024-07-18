using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaneSelectionManager : MonoBehaviour
{
    public ARRaycastManager raycastManager;
    public ARPlaneManager planeManager;
    public GameObject startCanvas;
    public GameObject startButton; // Renamed from 'Start' to avoid keyword conflict
    public GameObject capsulePrefab; // Renamed to clarify it's a prefab

    private ARPlane selectedPlane = null;

    void Update()
    {
        if (selectedPlane == null && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                List<ARRaycastHit> hits = new List<ARRaycastHit>();
                if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
                {
                    ARRaycastHit hit = hits[0];
                    selectedPlane = hit.trackable as ARPlane;

                    // Activate the start button
                    startCanvas.SetActive(true);

                    // Disable the ARPlaneManager and the visual representation of all planes except the selected one
                    planeManager.enabled = false;
                    foreach (ARPlane plane in planeManager.trackables)
                    {
                        if (plane != selectedPlane)
                        {
                            plane.gameObject.SetActive(false);
                            startButton.gameObject.SetActive(true);
                        }
                    }

                    Vector3 offset = new Vector3(0f, 0.5f, 0f);

                    // Calculate the spawn position
                    Vector3 spawnPosition = hit.pose.position + offset;

                    // Instantiate capsule at the calculated position with the hit rotation
                    Instantiate(capsulePrefab, spawnPosition, hit.pose.rotation);
                }
            }
        }
    }
}
