using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInputManager : MonoBehaviour
{
    private MainInputSystem playerInputActions;
    public static Vector2 MOVEMENT = Vector2.zero;
    public static bool JUMP_DOWN = false;
    public static bool JUMP_HELD = false;
    public static bool INTERACT = false;
    public static bool CROUCH = false;
    private bool _canMove = true;


    private void Awake(){
        playerInputActions = InputManager.playerInputActions;
    }

    private void OnEnable(){
        EnableInput();
        Manager_Event.InteractionManager.OnStartInteraction.Get().AddListener(DisableInput);
        Manager_Event.InteractionManager.OnEndInteraction.Get().AddListener(EnableInput);
        Manager_Event.InteractionManager.OnStartTimeline.Get().AddListener(DisableCanMove);
        Manager_Event.InteractionManager.OnEndTimeline.Get().AddListener(EnableCanMove);
    }

    private void OnDisable(){
        DisableInput();
        Manager_Event.InteractionManager.OnStartInteraction.Get().RemoveListener(DisableInput);
        Manager_Event.InteractionManager.OnEndInteraction.Get().RemoveListener(EnableInput);
        Manager_Event.InteractionManager.OnStartTimeline.Get().AddListener(DisableCanMove);
        Manager_Event.InteractionManager.OnEndTimeline.Get().AddListener(EnableCanMove);
    }

    private void Update(){
        if(!_canMove)
            return;

        //Movement
        MOVEMENT = playerInputActions.Player.Movement.ReadValue<Vector2>();
        JUMP_DOWN = playerInputActions.Player.Jump.WasPressedThisFrame();
        JUMP_HELD = playerInputActions.Player.Jump.IsPressed();
        CROUCH = playerInputActions.Player.Crouch.IsPressed();

        //Interaction
        INTERACT = playerInputActions.Player.Interact.WasPressedThisFrame();
    }

    private void EnableInput(){
        playerInputActions.Player.Enable();
    }
    private void DisableInput(){
        playerInputActions.Player.Disable();
    }

    private void EnableCanMove(){
        _canMove = true;
    }
    private void DisableCanMove(){
        _canMove = false;

        //Movement
        MOVEMENT = Vector2.zero;
        JUMP_DOWN = false;
        JUMP_HELD = false;
        CROUCH = false;

        //Interaction
        INTERACT = false;
    }
}
