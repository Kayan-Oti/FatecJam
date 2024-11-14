using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using MyBox;
using UnityEngine;
using UnityEngine.Playables;

public class LevelManager_Caverna : MonoBehaviour
{
    [SerializeField] private PlayableDirector _timelime;
    [SerializeField] private UI_ManagerAnimation _cutsceneAnimator;
    [SerializeField] private CinemachineImpulseSource _cameraShake;

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
        Manager_Event.InteractionManager.OnEndTimeline.Get().Invoke();
    }

    public void EndAnimationBlackStrips(){
        _cutsceneAnimator.PlayAnimation("End");
    }

    public void CavernaTrigger1(){
        _cameraShake.GenerateImpulseWithVelocity(new Vector3(1,-1,0));
    }
}
