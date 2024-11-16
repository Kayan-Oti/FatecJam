using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInputManager : MonoBehaviour
{
    private MainInputSystem playerInputActions;
    //Player Movement
    public static Vector2 MOVEMENT = Vector2.zero;
    public static bool JUMP_DOWN = false;
    public static bool JUMP_HELD = false;
    public static bool INTERACT = false;
    public static bool CROUCH = false;
    public static bool WALKING = false;
    private bool _canMove = true;

    //Player UI
    public static bool DIALOGUE_BUTTON_DOWN = false;


    private void Awake(){
        playerInputActions = InputManager.playerInputActions;
    }

    private void OnEnable(){
        SetInputState(true, false);
        Manager_Event.InteractionManager.OnStartInteraction.Get().AddListener(OnStartInteraction);
        Manager_Event.InteractionManager.OnEndInteraction.Get().AddListener(OnEndInteraction);
        Manager_Event.InteractionManager.OnStartTimeline.Get().AddListener(DisableCanMove);
        Manager_Event.InteractionManager.OnEndTimeline.Get().AddListener(EnableCanMove);
    }

    private void OnDisable(){
        SetInputState(false, false);
        Manager_Event.InteractionManager.OnStartInteraction.Get().RemoveListener(OnStartInteraction);
        Manager_Event.InteractionManager.OnEndInteraction.Get().RemoveListener(OnEndInteraction);
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

        //Dialogue
        DIALOGUE_BUTTON_DOWN = playerInputActions.PlayerUI.DialogueButton.WasPressedThisFrame();
    }

    private void OnStartInteraction(){
        SetInputState(false, true);
    }
    private void OnEndInteraction(){
        SetInputState(true, false);
    }

    private void SetInputState(bool statePlayer, bool statePLayerUI){
        //Player Inputs
        if(statePlayer)
            playerInputActions.Player.Enable();
        else
            playerInputActions.Player.Disable();
        //UI Inputs
        if(statePLayerUI)
            playerInputActions.PlayerUI.Enable();
        else
            playerInputActions.PlayerUI.Disable();
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
