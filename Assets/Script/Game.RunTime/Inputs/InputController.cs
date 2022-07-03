using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.InputSystem;
[Serializable]
public class MoveInputEvent: UnityEvent<float, float>{ }
[Serializable]
public class MousePosEvent : UnityEvent<Vector2> { }
[Serializable]
public class InputEventBool : UnityEvent<bool> { }
public class InputController : MonoBehaviour
{
    Controls controls;
    public MoveInputEvent moveInputEvent;
    public MousePosEvent mousePosEvent;
    public InputEventBool inputEventBool;
    private void Awake()
    {
        controls = new Controls();
    }
    private void OnEnable()
    {
        controls.GamePlay.Enable();
        controls.GamePlay.Move.performed += OnMove;
        controls.GamePlay.Move.canceled += OnMove;
        controls.GamePlay.MousePosition.performed += OnMousePos;
        controls.GamePlay.Fire.performed += OnFire;
        controls.GamePlay.Fire.canceled += OnFire;
    }
    private void OnDisable()
    {
        controls.GamePlay.Disable();
    }
    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();
        moveInputEvent.Invoke(moveInput.x, moveInput.y);
    }
    private void OnMousePos(InputAction.CallbackContext context)
    {
        Vector2 mousePos = context.ReadValue<Vector2>();
        mousePosEvent.Invoke(mousePos);
    }
    private void OnFire(InputAction.CallbackContext context)
    {
        bool fireInput = context.ReadValueAsButton();
        inputEventBool.Invoke(fireInput);
    }

}
