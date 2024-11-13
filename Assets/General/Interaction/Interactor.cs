using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Interactor : MonoBehaviour
{
    private List<IInteractable> _interactablesInRange = new List<IInteractable>();
    [ReadOnly] private int _numInteractablesInRange = 0;

    private void Update(){
        if(PlayerInputManager.INTERACT)
            TryInteract();
    }

    public void TryInteract(){
        IInteractable interactable = null;

        //Try Gets a interactable in Range
        while(_interactablesInRange.Count > 0){
            interactable = _interactablesInRange[0];

            if(interactable.CanInteract())
                break;
            else
                _interactablesInRange.Remove(interactable);
        }

        //If no interactables left in Range
        if(_interactablesInRange.Count == 0)
            return;

        interactable.Interact(this);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        IInteractable interactable = other.GetComponent<IInteractable>();

        if(interactable != null){
            _numInteractablesInRange++;
            if(!interactable.CanInteract())
                return;
            interactable.SetIsInRange(true);
            _interactablesInRange.Add(interactable);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        IInteractable interactable = other.GetComponent<IInteractable>();

        if(_interactablesInRange.Contains(interactable)){
            _numInteractablesInRange--;
            interactable.SetIsInRange(false);
            _interactablesInRange.Remove(interactable);
        }
    }

}
