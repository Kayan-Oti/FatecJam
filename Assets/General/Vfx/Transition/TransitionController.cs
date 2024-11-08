using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MyBox;
using UnityEngine;

public class TransitionController : MonoBehaviour
{
    [SerializeField] private Material material;
    private const float RADIUS =  1.2f;
    [SerializeField] private float _duration  =  1.0f;

    void Start()
    {
        UpdateShader(RADIUS);
    }

    public void FadeIn(Action doLast = null)
    {
        DoFade(RADIUS, 0f, doLast);
    }

    public void FadeOut(Action doLast = null)
    {
        DoFade(0, RADIUS, doLast);
    }

    private void DoFade(float start, float end, Action doLast = null)
    {
        UpdateShader(start);

        DOTween.To(() => start, x => UpdateShader(x), end, _duration)
            .OnComplete(() => doLast?.Invoke());
    }

    public void UpdateShader(float radius)
    {
        material.SetFloat("_CircleRadius", radius);
    }
}
