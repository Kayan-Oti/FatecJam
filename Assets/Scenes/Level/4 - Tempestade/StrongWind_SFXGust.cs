using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class StrongWind_SFXGust : MonoBehaviour
{
    private const string PARAMETER_NAME = "wind_intensity";
    private bool _inArea = false;

    public void SetIntensity(float intensity){
        if(!_inArea)
            return;
        AudioManager.Instance.SetAmbienceParameter(PARAMETER_NAME, intensity);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player"))
            _inArea = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            SetIntensity(0f);
            _inArea = false;
        }
    }
}
