using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    #region Private variables
    private InputSystem_Actions _inputActions;
    #endregion

    #region Public variables
    public static InputManager Instance { get; private set;}
    #endregion
	
    #region Lifecycle methods
	private void Awake(){
        if (Instance == null)
            Instance = this;
        else {
            Debug.LogError("There are more more than one InputManagers");
        }

        _inputActions = new InputSystem_Actions();
    }

    private void OnEnable() {
        _inputActions.Enable();
    }
    private void OnDisable() {
        _inputActions.Disable();
    }
    #endregion
	
    #region Private methods
	
    #endregion
	
    #region Public methods
	public Vector2 GetPlayerMovement() {
        return _inputActions.Player.Move.ReadValue<Vector2>();
    }

    public Vector2 GetMouseDelta() {
        return _inputActions.Player.Look.ReadValue<Vector2>();
    }
    #endregion
}
