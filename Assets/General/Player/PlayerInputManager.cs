using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInputManager : MonoBehaviour
{
    private MainInputSystem playerInputActions;
    public static Vector2 MOVEMENT;
    public static bool JUMP_DOWN;
    public static bool JUMP_HELD;
    public static bool INTERACT;

    private void Awake(){
        playerInputActions = InputManager.playerInputActions;
    }

    private void OnEnable(){
        EnableInput();
        Manager_Event.InteractionManager.OnStartInteraction.Get().AddListener(DisableInput);
        Manager_Event.InteractionManager.OnEndInteraction.Get().AddListener(EnableInput);
    }

    private void OnDisable(){
        DisableInput();
        Manager_Event.InteractionManager.OnStartInteraction.Get().RemoveListener(DisableInput);
        Manager_Event.InteractionManager.OnEndInteraction.Get().RemoveListener(EnableInput);
    }

    private void Update(){
        //Movement
        MOVEMENT = playerInputActions.Player.Movement.ReadValue<Vector2>();
        JUMP_DOWN = playerInputActions.Player.Jump.WasPressedThisFrame();
        JUMP_HELD = playerInputActions.Player.Jump.IsPressed();

        //Interaction
        INTERACT = playerInputActions.Player.Interact.WasPressedThisFrame();
    }

    private void EnableInput(){
        playerInputActions.Player.Enable();
    }
    private void DisableInput(){
        playerInputActions.Player.Disable();
    }
}
