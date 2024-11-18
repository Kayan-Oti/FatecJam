using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Food : Abstract_Interactable
{
    [SerializeField] private Collider2D _blockJanela;
    [SerializeField] private Collider2D _blockDialogue;
    [SerializeField] private InteractablesManager _manager;
    [SerializeField] public GameObject _displayObject;
    [SerializeField] protected Manager_Dialogue _managerDialogue;
    [SerializeField] protected SO_Dialogue _soDialogue;

    protected override void InteractionAction()
    {
        _manager.OnCollect(_displayObject, DisableCollectable);
    }

    public void DisableCollectable(){
        EndInteraction();
        _blockJanela.enabled = false;
        _blockDialogue.enabled = false;
        transform.parent.gameObject.SetActive(false);
        TriggerDialogue();
    }

    private void TriggerDialogue(){
        Manager_Event.InteractionManager.OnStartInteraction.Get().Invoke();
        _managerDialogue.StartDialogue(_soDialogue, EndDialogue);
    }

    private void EndDialogue(){
        Manager_Event.InteractionManager.OnEndInteraction.Get().Invoke();
        gameObject.SetActive(false);
    }

    protected override bool HasMoreInteraction()
    {
        return true;
    }
}
