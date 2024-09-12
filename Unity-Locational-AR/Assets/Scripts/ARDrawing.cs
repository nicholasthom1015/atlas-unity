using UnityEngine;

public class ARDrawing : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Camera arCamera; // Reference to the AR Camera

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
            {
                // Convert the touch position to a ray
                Ray ray = arCamera.ScreenPointToRay(touch.position);

                // Calculate a point in front of the camera to simulate the drawing plane
                // Assuming we want to draw on a plane that is 1 meter in front of the camera
                float distance = 1.0f; // Distance from the camera
                Vector3 drawPosition = ray.origin + ray.direction * distance;

                Draw(drawPosition);
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
