using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;
using UnityEngine.Playables;

public class LevelManager_Floresta : MonoBehaviour
{
    [SerializeField] private PlayableDirector _timelime;

    private void OnEnable() {
        Manager_Event.GameManager.OnLoadedScene.Get().AddListener(StartCutscene);
    }

    private void OnDisable(){
        Manager_Event.GameManager.OnLoadedScene.Get().RemoveListener(StartCutscene);
    }

    [ButtonMethod]
    public void StartCutscene(){
        _timelime.Play();
        Manager_Event.InteractionManager.OnStartTimeline.Get().Invoke();
    }

    [ButtonMethod]
    public void EndCutscene(){
        GetComponent<ControllPlayerByCode>().enabled = false;
        Manager_Event.InteractionManager.OnEndTimeline.Get().Invoke();
    }
}
