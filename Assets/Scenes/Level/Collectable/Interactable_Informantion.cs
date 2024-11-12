using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Informantion : Abstract_Interactable
{
    [SerializeField] private InteractablesManager _manager;
    [SerializeField] public Sprite _spriteDisplay;
    private bool _hasBeenCollected =  false;

    protected override void InteractionAction()
    {
        _manager.OnCollect(this);
        _hasBeenCollected = true;
    }

    public void DisableCollectable(){
        EndInteraction();
        transform.parent.gameObject.SetActive(false);
    }

    protected override bool HasMoreInteraction()
    {
        return !_hasBeenCollected;
    }
}
