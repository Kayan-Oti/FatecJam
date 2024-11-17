using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Collider2D))]
public class StrongWind : MonoBehaviour
{
    [SerializeField] private Vector2 windForce = new Vector2(5f, 0f); // Direção e força do vento
    [SerializeField] private bool isGust = false; // Vento em rajadas?
    [SerializeField] private float gustInterval = 2f; // Intervalo entre as rajadas
    [SerializeField] private float gustDuration = 1f; // Duração de cada rajada
    [Header("Wind Sfx")]
    [SerializeField] private StrongWind_SFXGust _sfxManager;
    [SerializeField] private float _windIntensity = 1f;
    [Header("Wind Particle")]
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private float _lerpDuration = 0.5f;
    [SerializeField] private float _targetSpeed = 0.5f;
    private bool isActive = true;
    private const float DEFAULT_PARTICLE_SPEED = 1f;
    private const float DEFAULT_SFX_INTENSITY = 0f;

    private void Start()
    {
        if (!isGust)
            return;
        if(_sfxManager == null || _particleSystem == null){
            Debug.LogError("GameObject missing");
            return;
        }
        
        StartCoroutine(GustController());
    }

    private IEnumerator GustController()
    {
        while (true)
        {
            isActive = true;
            ChangeEffectValues(_windIntensity, DEFAULT_PARTICLE_SPEED);
            yield return new WaitForSeconds(gustDuration);
            
            ChangeEffectValues(DEFAULT_SFX_INTENSITY, _targetSpeed);
            isActive = false;
            yield return new WaitForSeconds(gustInterval);
        }
    }

    private void ChangeEffectValues(float sfxIntensity, float particleSpeed){
        _sfxManager.SetIntensity(sfxIntensity);

        //Lerp simulation Speed
        var mainModule = _particleSystem.main; // Não pode ser diretamente modificado
        DOTween.To(() => mainModule.simulationSpeed, x => 
        {
            var tempMain = _particleSystem.main;
            tempMain.simulationSpeed = x;
        }, particleSpeed, _lerpDuration);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isActive && collision.TryGetComponent(out Rigidbody2D rb))
            rb.AddForce(windForce);
    }
}
