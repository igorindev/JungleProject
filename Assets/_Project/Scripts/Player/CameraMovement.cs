using UnityEngine;

public interface ICameraMovement
{
    void Setup(IPlayerInput playerInput);
}

public class CameraMovement : MonoBehaviour, ICameraMovement
{
    [SerializeField] Transform _centralPoint;
    [SerializeField] float _rotationSpeed = 2.0f;
    [SerializeField] float _acceleration = 1.0f;
    [SerializeField] float _maxSpeed = 10.0f;
    [SerializeField] float _lerpSpeed = 5f;

    [SerializeField] float _minRotationX = 0;
    [SerializeField] float _maxRotationX = 50;

    Vector3 _previousMousePosition;
    Vector3 _currentVelocity;

    Vector3 _deltaMouse;
    Vector3 _targetVelocity;

    float _currentRotationX = 0.0f;
    float _currentRotationY = 0.0f;

    IPlayerInput _playerInput;

    public void Setup(IPlayerInput playerInput)
    {
        _playerInput = playerInput;
        _playerInput.RightMouseDownWithMousePosition += SetMousePreviousPos;
        _playerInput.RightMouseHoldWithMousePosition += UpdateDelta;
        _playerInput.RightMouseUpWithMousePosition += ClearDelta;
    }

    void OnDestroy()
    {
        _playerInput.RightMouseDownWithMousePosition -= SetMousePreviousPos;
        _playerInput.RightMouseHoldWithMousePosition -= UpdateDelta;
        _playerInput.RightMouseUpWithMousePosition -= ClearDelta;
    }

    void Update()
    {
        RotateCamera(_deltaMouse);
    }

    void SetMousePreviousPos(Vector3 mousePos)
    {
        _previousMousePosition = mousePos;
    }

    void UpdateDelta(Vector3 mousePos)
    {
        _deltaMouse = mousePos - _previousMousePosition;
        _previousMousePosition = mousePos;
    }

    void ClearDelta(Vector3 _)
    {
        _deltaMouse = Vector3.zero;
    }

    void RotateCamera(Vector3 deltaMouse)
    {
        _targetVelocity = _acceleration * _rotationSpeed * new Vector2(-deltaMouse.x, deltaMouse.y);
        _targetVelocity = Vector3.ClampMagnitude(_targetVelocity, _maxSpeed);

        _currentVelocity = Vector3.Lerp(_currentVelocity, _targetVelocity, _lerpSpeed * Time.unscaledDeltaTime);

        _currentRotationX += _currentVelocity.y * Time.unscaledDeltaTime;
        _currentRotationY += _currentVelocity.x * Time.unscaledDeltaTime;

        _currentRotationX = Mathf.Clamp(_currentRotationX, _minRotationX, _maxRotationX);

        transform.localRotation = Quaternion.Euler(_currentRotationX, _currentRotationY, 0f);
    }
}
