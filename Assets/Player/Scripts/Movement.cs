using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
 
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    // camera stuff
    [SerializeField] private Transform _cameraParentTransform;
    private Vector3 _cameraStartPosition;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _cameraSprintDisplacement = 0.5f;

    // movement variables
    public float _movementSpeed = 5f;
    public float _rotationSpeed = 10f;
    public float _jumpForce = 5f;
    public float _sprintMultiplier = 2f;
    private bool _isSprinting = false;

    public int _jumpTotal = 1;
    private int _jumpCount = 0;

    // components
    private Rigidbody _rigidbody;
    private Vector2 _movementInput;
    private Vector2 _rotationInput;

    // ground check
    public LayerMask _groundLayer; // Assign the ground layer in the Inspector
    public float _groundCheckDistance = 1f;

    public enum Direction
    {
        Null,
        Forward,
        Backward,
        Left,
        Right,
        ForwardLeft,
        ForwardRight,
        BackwardLeft,
        BackwardRight
    }
    private Direction _direction;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _cameraStartPosition = _camera.transform.localPosition;
    }

    private void Update()
    {
        HandleRotation();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        float multiplier = 1f;
        if (_isSprinting) multiplier = _sprintMultiplier;

        Vector3 movementDirection = new Vector3(_movementInput.x, 0f, _movementInput.y);
        Vector3 movement = movementDirection.normalized * _movementSpeed * Time.fixedDeltaTime * multiplier;
        _rigidbody.MovePosition(_rigidbody.position + transform.TransformDirection(movement));
    }

    private void HandleRotation()
    {
        // Calculate the rotation amounts based on mouse delta
        float rotationAmountX = _rotationInput.x * _rotationSpeed * Time.fixedDeltaTime;
        float rotationAmountY = -_rotationInput.y * _rotationSpeed * Time.fixedDeltaTime; // Invert to match typical FPS camera behavior

        transform.Rotate(Vector3.up * rotationAmountX);
        _cameraParentTransform.Rotate(Vector3.right * rotationAmountY);
    }

    private void Jump()
    {
        if(_jumpCount < _jumpTotal)
        {
            _jumpCount++;
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
        else if (IsGrounded())
        {
            _jumpCount = 1;
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, _groundCheckDistance, _groundLayer);
    }

    public void OnMovementInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _movementInput = context.ReadValue<Vector2>();

            if (_movementInput.y > 0)
            {
                if (_movementInput.x > 0) _direction = Direction.ForwardRight;
                else if (_movementInput.x < 0) _direction = Direction.ForwardLeft;
                else _direction = Direction.Forward;
            }
            else if (_movementInput.y < 0)
            {
                if (_movementInput.x > 0) _direction = Direction.BackwardRight;
                else if (_movementInput.x < 0) _direction = Direction.BackwardLeft;
                else _direction = Direction.Backward;
            }
            else
            {
                if (_movementInput.x > 0) _direction = Direction.Right;
                else if (_movementInput.x < 0) _direction = Direction.Left;
                else _direction = Direction.Null;
            }
        }
        else if(context.canceled)
        {
            _movementInput = new Vector2(0, 0);
        }
    }

    public void OnRotationInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _rotationInput = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            _rotationInput = new Vector2(0, 0);
        }
    }
    
    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _isSprinting = true;
            _camera.transform.localPosition = new Vector3(_cameraStartPosition.x, _cameraStartPosition.y, _cameraStartPosition.z - _cameraSprintDisplacement);
        }
        else if (context.canceled)
        {
            _isSprinting = false;
            _camera.transform.localPosition = _cameraStartPosition;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Jump();
        }
    }

    public Direction GetDirection()
    {
        return _direction; 
    }
}
