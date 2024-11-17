using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using MyBox;
using UnityEngine;
using UnityEngine.Playables;

public class LevelManager_Final : MonoBehaviour
{
    [SerializeField] private PlayableDirector _timelime;
    [Header("Animators")]
    [SerializeField] private UI_ManagerAnimation _cutsceneAnimator;
    [SerializeField] private UI_ManagerAnimation _creditsAnimator;
    [Header("Dialogue")]
    [SerializeField] protected Manager_Dialogue _managerDialogue;
    [SerializeField] protected SO_Dialogue _soDialogue;

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

    #region Timeline Triggers

    //EndCutscene
    [ButtonMethod]
    public void EndCutscene(){
        _creditsAnimator.PlayAnimation("Start", EnableMenuButton);
    }
    //ChangeCamera
    public void EndAnimationBlackStrips(){
        _cutsceneAnimator.PlayAnimation("End");
    }

    //Trigger_LastDialogue

    public void LastDialogue(){
        _managerDialogue.StartDialogue(_soDialogue);
    }
    //Trigger1

    public void NextDialogue(){
        _managerDialogue.Onclick();
    }

    #endregion

    public void EnableMenuButton(){
        _creditsAnimator.PlayAnimation("ButtonMenu");
    }

    public void BackToMenu(){
        GameManager.Instance.LoadScene(SceneIndex.Menu);
    }
}
