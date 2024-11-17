using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager_Tempestade : MonoBehaviour
{
    private void Start() {
        AudioManager.Instance.StopAmbience();
        AudioManager.Instance.InitializeAmbience(FMODEvents.Instance.StrongWind);
    }
}
