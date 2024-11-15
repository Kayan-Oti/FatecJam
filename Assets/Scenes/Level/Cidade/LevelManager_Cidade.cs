using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;
using UnityEngine.Playables;

public class LevelManager_Cidade : MonoBehaviour
{
    [SerializeField] private PlayableDirector _timelime;
    [SerializeField] private UI_ManagerAnimation _cutsceneAnimator;
    [SerializeField] private Collider2D _blockCollider;

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
        _blockCollider.enabled = true;
        GetComponent<ControllPlayerByCode>().enabled = false;
        Manager_Event.InteractionManager.OnEndTimeline.Get().Invoke();
    }

    public void EndAnimationBlackStrips(){
        _cutsceneAnimator.PlayAnimation("End");
    }
}
