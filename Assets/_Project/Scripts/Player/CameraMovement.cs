using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform centralPoint;
    [SerializeField] float rotationSpeed = 2.0f;
    [SerializeField] float acceleration = 1.0f;
    [SerializeField] float maxSpeed = 10.0f;
    [SerializeField] float _lerpSpeed = 5f;

    [SerializeField] float _minRotationX = 0;
    [SerializeField] float _maxRotationX = 50;

    Vector3 _previousMousePosition;
    Vector3 _currentVelocity;

    Vector3 _deltaMouse;
    Vector3 _targetVelocity;

    float _currentRotationX = 0.0f;
    float _currentRotationY = 0.0f;

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;

        if (Input.GetMouseButtonDown(1))
        {
            _previousMousePosition = mousePos;
        }

        if (Input.GetMouseButton(1))
        {
            _deltaMouse = mousePos - _previousMousePosition;
            _previousMousePosition = mousePos;
        }
        else
        {
            _deltaMouse = Vector3.zero;
        }

        RotateCamera(_deltaMouse);
    }

    void RotateCamera(Vector3 deltaMouse)
    {
        _targetVelocity = new Vector2(-deltaMouse.x, deltaMouse.y) * rotationSpeed * acceleration;
        _targetVelocity = Vector3.ClampMagnitude(_targetVelocity, maxSpeed);

        _currentVelocity = Vector3.Lerp(_currentVelocity, _targetVelocity, _lerpSpeed * Time.unscaledDeltaTime);

        _currentRotationX += _currentVelocity.y * Time.unscaledDeltaTime;
        _currentRotationY += _currentVelocity.x * Time.unscaledDeltaTime;

        _currentRotationX = Mathf.Clamp(_currentRotationX, _minRotationX, _maxRotationX);

        transform.localRotation = Quaternion.Euler(_currentRotationX, _currentRotationY, 0f);
    }
}
