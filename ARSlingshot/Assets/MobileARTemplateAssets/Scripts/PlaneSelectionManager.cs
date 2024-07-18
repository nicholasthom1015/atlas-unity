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
    private ARPlane selectedPlane = null;
    public GameObject Start;
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

                    // Optionally, activate the start button
                    startCanvas.SetActive(true);

                    // Disable the ARPlaneManager and the visual representation of all planes
                    planeManager.enabled = false;
                    foreach (ARPlane plane in planeManager.trackables)
                    {
                        if (plane != selectedPlane)
                        {
                            plane.gameObject.SetActive(false);
                            Start.gameObject.SetActive(true);
                            //Instantiate(Capsule, hit.position, hit.rotation);
                        }

                    }
                }
            }
        }
    }
}
