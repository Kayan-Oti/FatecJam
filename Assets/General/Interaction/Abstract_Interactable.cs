using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Abstract_Interactable : MonoBehaviour, IInteractable
{
    [SerializeField] protected GameObject _interactionText;
    protected bool _canInteract = true;
    [SerializeField] protected bool _loop = false;

    protected virtual void Start(){
        _interactionText.SetActive(false);
    }

    public void Interact(Interactor interactor)
    {
        if(!CanInteract())
            return;

        Manager_Event.InteractionManager.OnStartInteraction.Get().Invoke();
        InteractionAction();
    }

    public void EndInteraction()
    {
        Manager_Event.InteractionManager.OnEndInteraction.Get().Invoke();
        CanInteract();
        SetInteractionText(_canInteract);
    }

    public virtual bool CanInteract(){
        _canInteract = HasMoreInteraction();
        return _canInteract;
    }

    public virtual void SetIsInRange(bool state){
        if(!CanInteract())
            return;

        SetInteractionText(state);
    }

    private void SetInteractionText(bool state){
        _interactionText.SetActive(state);
    }

    protected abstract void InteractionAction();
    protected abstract bool HasMoreInteraction();

}
