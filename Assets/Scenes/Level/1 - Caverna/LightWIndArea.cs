using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class LightWIndArea : MonoBehaviour
{
    [Header("Wind Intensity")]
    [SerializeField] private float _windIntensity = .5f;
    private const string PARAMETER_NAME = "wind_intensity";

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player"))
            AudioManager.Instance.SetAmbienceParameter(PARAMETER_NAME, _windIntensity);
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player"))
             AudioManager.Instance.SetAmbienceParameter(PARAMETER_NAME, 0f);
    }
}
