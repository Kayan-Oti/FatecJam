using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue_Trigger : Abstract_Interactable
{
    [SerializeField] private Manager_Dialogue _manager;
    [SerializeField] private List<SO_Dialogue> _soDialogue;
    private int _currentDialogue = 0;

    protected override bool HasMoreInteraction()
    {
        return _currentDialogue < _soDialogue.Count;
    }

    protected override void InteractionAction()
    {
        _manager.StartDialogue(_soDialogue[_currentDialogue], EndInteraction);
        if(!_soDialogue[_currentDialogue].loop)
            _currentDialogue++;
    }
}