using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    public float yToZVelocityRatio = 0.5f; // Adjust this ratio to scale Y velocity to Z velocity
    public float maxYVelocity = 10f; // Maximum allowed Y velocity

    private Camera _camera;
    private bool _isDragging = false;
    private Vector3 _offset;
    private float _zCoord;
    private Vector3 _previousPosition;
    private Vector3 _velocity;
    private Rigidbody _rigidbody;

    void Start()
    {
        _camera = Camera.main;
        _rigidbody = GetComponent<Rigidbody>();
        _previousPosition = transform.position;
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
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = _zCoord; // Use the z coordinate from OnMouseDown
        return _camera.ScreenToWorldPoint(mouseScreenPosition);
    }
}
