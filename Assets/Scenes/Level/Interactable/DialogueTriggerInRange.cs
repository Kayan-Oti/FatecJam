using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerInRange : MonoBehaviour
{
    [SerializeField] protected Manager_Dialogue _manager;
    [SerializeField] protected SO_Dialogue _soDialogue;

    private void OnTriggerEnter2D(Collider2D other) {
        Manager_Event.InteractionManager.OnStartInteraction.Get().Invoke();
        _manager.StartDialogue(_soDialogue, EndDialogue);
    }

    private void EndDialogue(){
        Manager_Event.InteractionManager.OnEndInteraction.Get().Invoke();
        if(!_soDialogue.loop)
            gameObject.SetActive(false);
    }
}
