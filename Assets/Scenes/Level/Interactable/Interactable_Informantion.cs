using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Informantion : Abstract_Interactable
{
    [SerializeField] private InteractablesManager _manager;
    [SerializeField] public GameObject _displayObject;
    private bool _hasBeenCollected =  false;

    protected override void InteractionAction()
    {
        _hasBeenCollected = true;
        // _manager.OnCollect(this);
    }

    public void DisableCollectable(){
        EndInteraction();
        _displayObject.SetActive(false);
        transform.parent.gameObject.SetActive(false);
    }

    protected override bool HasMoreInteraction()
    {
        return !_hasBeenCollected;
    }
}
