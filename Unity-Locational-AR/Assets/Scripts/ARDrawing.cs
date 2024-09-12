using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARDrawing : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public ARRaycastManager arRaycastManager;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
            {
                // Convert the touch position to a Vector2
                Vector2 touchPosition = touch.position;

                // Perform the raycast
                List<ARRaycastHit> hitResults = new List<ARRaycastHit>();
                if (arRaycastManager.Raycast(touchPosition, hitResults, TrackableType.PlaneWithinPolygon))
                {
                    if (hitResults.Count > 0)
                    {
                        Pose hitPose = hitResults[0].pose;
                        Draw(hitPose.position);
                    }
                }
            }
        }
    }

    private void Draw(Vector3 position)
    {
        if (lineRenderer.positionCount == 0)
        {
            lineRenderer.positionCount = 1;
            lineRenderer.SetPosition(0, position);
        }
        else
        {
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, position);
        }
    }
}
