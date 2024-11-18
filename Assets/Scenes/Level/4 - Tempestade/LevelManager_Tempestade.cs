using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;
using UnityEngine.Playables;

public class LevelManager_Tempestade : MonoBehaviour
{
    // [SerializeField] private PlayableDirector _timelime;
    // private void OnEnable() {
    //     Manager_Event.GameManager.OnLoadedScene.Get().AddListener(StartCutscene);
    // }

    // private void OnDisable(){
    //     Manager_Event.GameManager.OnLoadedScene.Get().RemoveListener(StartCutscene);
    // }
    
    private void Start() {
        AudioManager.Instance.StopAmbience();
        AudioManager.Instance.InitializeAmbience(FMODEvents.Instance.StrongWind);
        AudioManager.Instance.InitializeMusic(FMODEvents.Instance.TempestadeMusic, MusicIntensity.Intensity3);
    }

    // [ButtonMethod]
    // public void StartCutscene(){
    //     _timelime.Play();
    //     Manager_Event.InteractionManager.OnStartTimeline.Get().Invoke();
    // }

    // #region Timeline Triggers
    // //EndCutscene
    // [ButtonMethod]
    // public void EndCutscene(){
    //     AudioManager.Instance.InitializeMusic(FMODEvents.Instance.TempestadeMusic, MusicIntensity.Intensity3);
    //     GetComponent<ControllPlayerByCode>().enabled = false;
    //     Manager_Event.InteractionManager.OnEndTimeline.Get().Invoke();
    // }
    // #endregion
}