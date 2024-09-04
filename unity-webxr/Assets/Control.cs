using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    public float yToZVelocityRatio = 0.5f; // Adjust this ratio to scale Y velocity to Z velocity
    public float maxYVelocity = 10f; // Maximum allowed Y velocity
    public float xAxisControlSpeed = 5f; // Speed at which the ball moves along the X axis when controlled by 'A' and 'D'
    public float cameraFollowSpeed = 5f; // Speed at which the camera follows the ball
    public Vector3 cameraOffset = new Vector3(0, 5, -10); // Offset from the ball position

    public float initialZVelocity = 0f; // Initial Z-axis velocity
    public float zAxisAcceleration = 1f; // Acceleration applied to the Z-axis velocity
    public float maxZVelocity = 20f; // Maximum Z-axis velocity

    private Camera _camera;
    private bool _isDragging = false;
    private Vector3 _offset;
    private float _zCoord;
    private Vector3 _previousPosition;
    private Vector3 _velocity;
    private Rigidbody _rigidbody;
    private bool _isMoving = false;
    private float _currentZVelocity;

    void Start()
    {
        _camera = Camera.main;
        _rigidbody = GetComponent<Rigidbody>();
        _previousPosition = transform.position;
        _currentZVelocity = initialZVelocity; // Initialize Z velocity
    }

    void OnMouseDown()
    {
        _isDragging = true;
        _zCoord = _camera.WorldToScreenPoint(transform.position).z;
        _offset = transform.position - GetMouseWorldPosition();
        _previousPosition = transform.position; // Initialize previous position

        // Set Rigidbody to kinematic while dragging
        if (_rigidbody != null)
        {
            _rigidbody.isKinematic = true;
        }
    }

    void OnMouseUp()
    {
        _isDragging = false;

        // Set Rigidbody to non-kinematic when releasing the mouse
        if (_rigidbody != null)
        {
            _rigidbody.isKinematic = false;

            // Calculate Z velocity based on Y velocity
            float zVelocity = _velocity.y * yToZVelocityRatio;

            // Clamp Y velocity to maxYVelocity
            float clampedYVelocity = Mathf.Clamp(_velocity.y, -maxYVelocity, maxYVelocity);

            // Set new velocity for the Rigidbody
            Vector3 newVelocity = new Vector3(_velocity.x, clampedYVelocity, zVelocity);
            _rigidbody.velocity = newVelocity;

            // Indicate that the ball is now moving
            _isMoving = true;
        }
    }

    void Update()
    {
        if (_isDragging)
        {
            Vector3 mousePosition = GetMouseWorldPosition() + _offset;
            transform.position = mousePosition;

            // Calculate velocity while dragging
            _velocity = (transform.position - _previousPosition) / Time.deltaTime;

            // Update previous position
            _previousPosition = transform.position;
        }

        if (_isMoving)
        {
            // Handle X-axis control using 'A' and 'D' keys
            float horizontalInput = Input.GetAxis("Horizontal"); // Maps to 'A' and 'D' keys

            // Apply acceleration to Z-axis velocity
            _currentZVelocity = Mathf.Clamp(_currentZVelocity + zAxisAcceleration * Time.deltaTime, initialZVelocity, maxZVelocity);

            // Update Rigidbody velocity
            Vector3 currentVelocity = _rigidbody.velocity;
            _rigidbody.velocity = new Vector3(horizontalInput * xAxisControlSpeed, currentVelocity.y, _currentZVelocity);

            // Update camera position to follow the ball
            Vector3 targetCameraPosition = transform.position + cameraOffset;
            _camera.transform.position = Vector3.Lerp(_camera.transform.position, targetCameraPosition, cameraFollowSpeed * Time.deltaTime);
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = _zCoord; // Use the z coordinate from OnMouseDown
        return _camera.ScreenToWorldPoint(mouseScreenPosition);
    }
}
