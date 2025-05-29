using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    private Controls controls;

    public Vector2 MovementValue {  get; private set; }
    public Vector2 LookValue { get; private set; }

    //public event Action AttackEvent;
    public event Action TargetEvent;
    public event Action CancelTargetEvent;

    public bool IsAttacking = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Make the Player action map speak with this Input reader
        controls = new Controls();
        controls.Player.SetCallbacks(this);
        controls.Player.Enable();
    }

    private void OnDestroy()
    {//If we dont tidy we will get memory leaks.
        controls.Player.Disable();
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue=context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        LookValue=context.ReadValue<Vector2>();
        Debug.Log(LookValue);
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            IsAttacking = true;
        }
        else if(context.canceled)
        {
            IsAttacking = false;
        }
    }

    public void OnTarget(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }

        TargetEvent?.Invoke();
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        if(!context.performed) return;

        CancelTargetEvent?.Invoke();
    }
}
