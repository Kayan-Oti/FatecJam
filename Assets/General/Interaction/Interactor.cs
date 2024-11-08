using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Interactor : MonoBehaviour
{
    private List<IInteractable> _interactablesInRange = new List<IInteractable>();
    [ReadOnly] private int _numInteractablesInRange = 0;
    private bool _canInteract = true;

    private void OnEnable(){
        Manager_Event.InteractionManager.OnStartInteraction.Get().AddListener(DisableInteraction);
        Manager_Event.InteractionManager.OnEndInteraction.Get().AddListener(EnableInteraction);
    }

    private void OnDisable(){
        Manager_Event.InteractionManager.OnStartInteraction.Get().RemoveListener(DisableInteraction);
        Manager_Event.InteractionManager.OnEndInteraction.Get().RemoveListener(EnableInteraction);
    }

    public void TryInteract(){
        if(!_canInteract)
            return;

        IInteractable interactable = null;

        //Try Gets a interactable in Range
        while(_interactablesInRange.Count > 0){
            interactable = _interactablesInRange[0];

            if(interactable.CanInteract())
                break;

            _interactablesInRange.Remove(interactable);
        }

        //If no interactables left in Range
        if(_interactablesInRange.Count == 0)
            return;

        interactable.Interact(this);
    }
    public void SetInteractState(bool state){
        _canInteract = state;
    }

    private void DisableInteraction(){
        SetInteractState(false);
    }
    private void EnableInteraction(){
        SetInteractState(true);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        _numInteractablesInRange++;
        IInteractable interactable = other.GetComponent<IInteractable>();

        if(interactable != null){
            if(!interactable.CanInteract())
                return;
            interactable.SetIsInRange(true);
            _interactablesInRange.Add(interactable);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        _numInteractablesInRange--;
        IInteractable interactable = other.GetComponent<IInteractable>();

        if(_interactablesInRange.Contains(interactable)){
            interactable.SetIsInRange(false);
            _interactablesInRange.Remove(interactable);
        }
    }

}
