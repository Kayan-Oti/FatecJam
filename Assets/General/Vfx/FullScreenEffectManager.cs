using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MyBox;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FullScreenEffectManager : MonoBehaviour
{
   [Header("Time Stats")]
   [SerializeField] private float _hurtDisplayTime = 1.5f;
   [SerializeField] private float _hurtDFadeOutTime = 0.5f;

   [Header("References")]
   [SerializeField] private ScriptableRendererFeature _fullScreenDamage;
   [SerializeField] private Material _material;

   [Header("Intensity")]
   [SerializeField] private float _voronoiIntensityStartAmount  = 1.25f;
   [SerializeField] private float _vignetteIntensityStartAmount  = 1.25f;

   private int  _voronoiIntensity  = Shader.PropertyToID("_VoronoiIntensity");
   private int  _vignetteIntensity  = Shader.PropertyToID("_VignetteIntensity");

   private void Start() {
        _fullScreenDamage.SetActive(false);
   }

   [ButtonMethod]
   private void Hurt()
    {
        // Ativa o efeito de dano na tela
        _fullScreenDamage.SetActive(true);

        // Ajusta os valores iniciais
        _material.SetFloat(_voronoiIntensity, _voronoiIntensityStartAmount);
        _material.SetFloat(_vignetteIntensity, _vignetteIntensityStartAmount);

        // Exibe o efeito por um período antes de iniciar o fade out
        DOVirtual.DelayedCall(_hurtDisplayTime, () =>
        {
            _material.DOFloat(0f, _voronoiIntensity, _hurtDFadeOutTime);
            _material.DOFloat(0f, _vignetteIntensity, _hurtDFadeOutTime)
                .OnComplete(() =>
                {
                    // Desativa o efeito de dano quando o fade out termina
                    _fullScreenDamage.SetActive(false);
                });
        });
    }
}
