using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using MyBox;
using UnityEngine;
using UnityEngine.Playables;

public class LevelManager_Caverna : MonoBehaviour
{
    [SerializeField] private PlayableDirector _timelime;
    [SerializeField] private UI_ManagerAnimation _cutsceneAnimator;
    [SerializeField] private CinemachineImpulseSource _cameraShake;
    [SerializeField] private CanvasGroup _moveTutorial;

    private void OnEnable() {
        Manager_Event.GameManager.OnLoadedScene.Get().AddListener(StartCutscene);
    }

    private void OnDisable(){
        Manager_Event.GameManager.OnLoadedScene.Get().RemoveListener(StartCutscene);
    }

    private void Start() {
        _moveTutorial.alpha = 0;
    }

    [ButtonMethod]
    public void StartCutscene(){
        _timelime.Play();
        Manager_Event.InteractionManager.OnStartTimeline.Get().Invoke();
    }

    #region Timeline Triggers

    [ButtonMethod]
    public void EndCutscene(){
        Manager_Event.InteractionManager.OnEndTimeline.Get().Invoke();
        StartCoroutine(TutorialMove());
    }
    public void EndAnimationBlackStrips(){
        _cutsceneAnimator.PlayAnimation("End");
    }
    public void CavernaTrigger1(){
        _cameraShake.GenerateImpulseWithVelocity(new Vector3(1,-1,0));
    }

    #endregion

    private IEnumerator TutorialMove(){
        _moveTutorial.DOFade(1, 1);
        while(PlayerInputManager.MOVEMENT.x == 0){
            yield return null;
        }
        _moveTutorial.DOKill();
        _moveTutorial.DOFade(0,1);
    }
}
