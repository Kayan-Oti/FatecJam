using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue_Trigger : Abstract_Interactable
{
    [SerializeField] protected Manager_Dialogue _manager;
    [SerializeField] protected List<SO_Dialogue> _soDialogue;
    protected Action _extraEndAction;
    protected int _currentDialogue = 0;

    protected override bool HasMoreInteraction()
    {
        return _currentDialogue < _soDialogue.Count;
    }

    protected override void InteractionAction()
    {
        _manager.StartDialogue(_soDialogue[_currentDialogue], () => {
            EndInteraction();
            _extraEndAction?.Invoke();
        });
        if(!_soDialogue[_currentDialogue].loop)
            _currentDialogue++;
    }
}