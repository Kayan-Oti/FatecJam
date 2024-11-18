using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;
using UnityEngine.Playables;

public class LevelManager_Cidade : MonoBehaviour
{
    [Header("Timelines")]
    [SerializeField] private PlayableDirector _timelime;
    [SerializeField] private PlayableDirector _timelime2;

    [Header("Animators")]
    [SerializeField] private UI_ManagerAnimation _cutsceneAnimator;

    [Header("Dialogue")]
    [SerializeField] protected Manager_Dialogue _managerDialogue;
    [SerializeField] protected SO_Dialogue _soDialogue;

    [Header("Others")]
    [SerializeField] private Collider2D _blockCollider;

    private void OnEnable() {
        Manager_Event.GameManager.OnLoadedScene.Get().AddListener(StartCutscene1);
    }

    private void OnDisable(){
        Manager_Event.GameManager.OnLoadedScene.Get().RemoveListener(StartCutscene1);
    }

    [ButtonMethod]
    public void StartCutscene1(){
        _timelime.Play();
        Manager_Event.InteractionManager.OnStartTimeline.Get().Invoke();
    }

    [ButtonMethod]
    public void StartCutscene2(){
        _timelime2.Play();
        Manager_Event.InteractionManager.OnStartTimeline.Get().Invoke();
    }

    #region Timeline Triggers
    //EndCutscene
    [ButtonMethod]
    public void EndCutscene1(){
        AudioManager.Instance.InitializeMusic(FMODEvents.Instance.CidadeMusic, MusicIntensity.Intensity3);
        _blockCollider.enabled = true;
        GetComponent<ControllPlayerByCode>().enabled = false;
        Manager_Event.InteractionManager.OnEndTimeline.Get().Invoke();
    }
    //EndCutscene
    [ButtonMethod]
    public void EndCutscene2(){
        StartAnimationBlackStrips();
        Manager_Event.InteractionManager.OnEndTimeline.Get().Invoke();
    }

    public void StartAnimationBlackStrips(){
        _cutsceneAnimator.PlayAnimation("Start");
    }

    //ChangeCamera
    public void EndAnimationBlackStrips(){
        _cutsceneAnimator.PlayAnimation("End");
    }

    //Trigger_LastDialogue

    public void StartDialogue(){
        _managerDialogue.StartDialogue(_soDialogue);
    }
    //Trigger1

    public void NextDialogue(){
        _managerDialogue.Onclick();
    }

    #endregion

}
