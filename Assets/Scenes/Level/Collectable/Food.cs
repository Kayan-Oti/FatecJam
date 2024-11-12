using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Food : Abstract_Interactable
{
    private bool _hasBeenCollected =  false;

    protected override void InteractionAction()
    {
        _hasBeenCollected = true;
        DisableCollectable();
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
