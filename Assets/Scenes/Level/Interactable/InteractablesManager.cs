using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractablesManager : MonoBehaviour
{
    [SerializeField] private UI_ManagerAnimation _animator;
    [SerializeField] private GameObject _button;
    private Interactable_Informantion _currentInteractable;

    private void Start() {
        _button.SetActive(false);
    }

    public void OnCollect(Interactable_Informantion interactable){
        Manager_Event.InteractionManager.OnStartInteraction.Get().Invoke();
        _animator.PlayAnimation("Start", () => _button.SetActive(true));

        _currentInteractable = interactable;
        interactable._displayObject.SetActive(true);
    }

    public void OnEndCollect(){
        _button.SetActive(false);
        _animator.PlayAnimation("End", () => {
            Manager_Event.InteractionManager.OnEndInteraction.Get().Invoke();
            _currentInteractable.DisableCollectable();
            _currentInteractable = null;
        });
    }
}
