using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TutorialInteract : MonoBehaviour
{
    [SerializeField] CanvasGroup _tutorialUI;

    private void Start() {
        _tutorialUI.alpha = 0f;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            EnableUI();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            DisableUI();
        }
    }

    private void EnableUI(){
        _tutorialUI.DOKill();
        _tutorialUI.DOFade(1, 1);
    }

    private void DisableUI(Action DoLast = null){
        _tutorialUI.DOKill();
        Tween animation = _tutorialUI.DOFade(0,1);
        if(DoLast != null)
            animation.OnComplete(() => DoLast());
    }

    public void DisableTutorial(){
        GetComponent<Collider2D>().enabled = false;
        DisableUI(() => enabled = false);
    }
}
