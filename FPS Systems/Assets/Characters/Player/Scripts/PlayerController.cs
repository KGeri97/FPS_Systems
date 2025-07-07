using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region Private variables
    [Header("Movement")]
    [SerializeField]
    private float _movementSpeed;

    [Header("Jump")]
    [SerializeField]
    private float _jumpForce;
    [SerializeField]
    private int _maxJumps;
    private int _numberOfJumps;
    private bool _isGrounded;
    private bool _isJumpReleased;

    [Header("Look")]
    [SerializeField]
    private float _lookSensitivityHorizontal;
    [SerializeField]
    private float _lookSensitivityVertical;
    [SerializeField]
    private float _maxVerticalLookAngle;
    [SerializeField]
    private Transform _fpsCameraTransform;
    private float _cameraPitch = 0;

    [SerializeField]
    private Rigidbody _rigidbody;

    private InputManager _inputManager;

    #endregion

    #region Public variables

    #endregion

    #region Lifecycle methods
    private void Awake(){
        _numberOfJumps = _maxJumps;
	}
	
    private void Start(){
        _inputManager = InputManager.Instance;
    }

    private void Update(){

    }

    private void FixedUpdate() {
        Move();
        Jump();
        Look();
    }
    #endregion

    #region Private methods
    private void Move() {
        Vector2 movementInputVector = _inputManager.GetPlayerMovement();
        Vector3 movementVector = transform.forward * movementInputVector.y + transform.right * movementInputVector.x;
        _rigidbody.AddForce(movementVector * _movementSpeed * Time.fixedDeltaTime, ForceMode.Force);
    }

    //If there are more trigger colliders I might need to dedicate its own gameobject for this
    private void OnTriggerEnter(Collider other) {
        //Debug.Log($"Collision {other.gameObject.name}");
        _isGrounded = true;
        _numberOfJumps = _maxJumps;
    }

    private void OnTriggerExit(Collider other) {
        _isGrounded = false;
    }

    private void Jump() {
        if (_inputManager.GetJump() && _isJumpReleased && (_isGrounded || _numberOfJumps > 0)) {
            _isJumpReleased = false;
            _numberOfJumps--;
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
        else if (!_inputManager.GetJump())
            _isJumpReleased = true;

            
    }

    private void Look() {
        Vector2 lookInputVector = _inputManager.GetMouseDelta();

        _cameraPitch += lookInputVector.y * _lookSensitivityVertical * Time.fixedDeltaTime;
        _cameraPitch = Mathf.Clamp(_cameraPitch, -_maxVerticalLookAngle, _maxVerticalLookAngle);

        _fpsCameraTransform.eulerAngles = new Vector3(_cameraPitch, _fpsCameraTransform.eulerAngles.y , 0);
        transform.eulerAngles += new Vector3(0, lookInputVector.x * _lookSensitivityHorizontal * Time.fixedDeltaTime, 0);
    }
    #endregion

    #region Public methods

    #endregion
}
