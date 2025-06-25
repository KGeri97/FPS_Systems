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
	}
	
    private void Start(){
        _inputManager = InputManager.Instance;
    }

    private void Update(){

    }

    private void FixedUpdate() {
        Move();
        Look();
    }
    #endregion

    #region Private methods
    private void Move() {
        Vector2 movementInputVector = _inputManager.GetPlayerMovement();
        Vector3 movementVector = transform.forward * movementInputVector.y + transform.right * movementInputVector.x;
        _rigidbody.AddForce(movementVector * _movementSpeed * Time.fixedDeltaTime, ForceMode.Force);
    }

    private void Look() {
        Vector2 lookInputVector = _inputManager.GetMouseDelta();
        //Vector3 startingOrientation = _fpsCameraTransform.eulerAngles;
        //startingOrientation.y += lookInputVector.x * _lookSensitivityHorizontal * Time.fixedDeltaTime;
        //startingOrientation.x += lookInputVector.y * _lookSensitivityVertical * Time.fixedDeltaTime;
        //startingOrientation.x = Mathf.Clamp(startingOrientation.x, -_maxVerticalLookAngle, _maxVerticalLookAngle);

        _cameraPitch += lookInputVector.y * _lookSensitivityVertical * Time.fixedDeltaTime;
        _cameraPitch = Mathf.Clamp(_cameraPitch, -_maxVerticalLookAngle, _maxVerticalLookAngle);

        _fpsCameraTransform.eulerAngles = new Vector3(_cameraPitch, _fpsCameraTransform.eulerAngles.y , 0);
        transform.eulerAngles += new Vector3(0, lookInputVector.x * _lookSensitivityHorizontal * Time.fixedDeltaTime, 0);
    }
    #endregion

    #region Public methods

    #endregion
}
