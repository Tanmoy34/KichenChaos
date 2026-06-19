
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PLayerGameInput : MonoBehaviour
{

    public event EventHandler OnIntractAction;
    private PlayerController playerInputActions;
    private Vector2 movementInput;

    void Awake()
    {

        playerInputActions = new PlayerController();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Interact.performed += Interact_performed;

    }

     private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if(OnIntractAction != null)
        {
            OnIntractAction(this, EventArgs.Empty);
        }
        
    }









    public Vector2 GetMovementVectoNormalized()
    {
        

        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        


        inputVector = inputVector.normalized;
        return inputVector;
    }


}
