using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;
using UnityEngine.Playables;

public class LevelManager_Floresta : MonoBehaviour
{
    [SerializeField] private PlayableDirector _timelime;
    [SerializeField] private UI_ManagerAnimation _cutsceneAnimator;
    private const string PARAMETER_NAME = "wind_intensity";

    private void OnEnable() {
        Manager_Event.GameManager.OnLoadedScene.Get().AddListener(StartCutscene);
    }

    private void OnDisable(){
        Manager_Event.GameManager.OnLoadedScene.Get().RemoveListener(StartCutscene);
    }

    private void Start() {
        AudioManager.Instance.StopAmbience();
        AudioManager.Instance.InitializeAmbience(FMODEvents.Instance.WindAmbience);
        AudioManager.Instance.SetAmbienceParameter(PARAMETER_NAME, 0.5f);

    }

    [ButtonMethod]
    public void StartCutscene(){
        AudioManager.Instance.InitializeMusic(FMODEvents.Instance.FlorestaMusic, MusicIntensity.Intensity3);
        _timelime.Play();
        Manager_Event.InteractionManager.OnStartTimeline.Get().Invoke();
    }

    [ButtonMethod]
    public void EndCutscene(){
        GetComponent<ControllPlayerByCode>().enabled = false;
        Manager_Event.InteractionManager.OnEndTimeline.Get().Invoke();
    }

    public void EndAnimationBlackStrips(){
        _cutsceneAnimator.PlayAnimation("End");
    }
}
