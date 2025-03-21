using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;

public class ControllPlayerByCode : MonoBehaviour
{
    public Vector2 Movement = Vector2.zero;
    public bool IsWalking = false;

    private void Start(){
        Movement = Vector2.zero;
    }

    private void Update() {
        PlayerInputManager.MOVEMENT = Movement;
        PlayerInputManager.WALKING = IsWalking;
    }
}
