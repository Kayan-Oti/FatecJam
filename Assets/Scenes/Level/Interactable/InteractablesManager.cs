using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractablesManager : MonoBehaviour
{
    [SerializeField] private UI_ManagerAnimation _animator;
    [SerializeField] private GameObject _button;
    private Action dolast = null;

    private void Start() {
        _button.SetActive(false);
    }

    public void OnCollect(GameObject _displayObject, Action Dolast = null){
        Manager_Event.InteractionManager.OnStartInteraction.Get().Invoke();
        _animator.PlayAnimation("Start", () => _button.SetActive(true));

        dolast = Dolast;
        _displayObject.SetActive(true);
    }

    public void OnEndCollect(){
        _button.SetActive(false);
        _animator.PlayAnimation("End", () => {
            Manager_Event.InteractionManager.OnEndInteraction.Get().Invoke();
            dolast?.Invoke();
        });
    }
}
