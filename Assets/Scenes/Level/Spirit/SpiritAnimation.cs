using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritAnimation : MonoBehaviour
{
    public float hoverAmount = 0.5f; // Quantidade de movimento para cima e para baixo
    public float hoverSpeed = 2f; // Duração do movimento de subida/descida
    private Vector3 initialPosition;

    private void Start() {
        initialPosition = transform.localPosition;
    }
    private void Update()
    {
        // Inicia o movimento de subida e descida (efeito hover) com DOTween
        HoverEffect();
    }
    private void HoverEffect()
    {
        // Aplica uma flutuação no eixo Y usando uma função senoidal para o fantasma (filho)
        float yOffset = Mathf.Sin(Time.time * hoverSpeed) * hoverAmount;
        transform.localPosition = initialPosition + new Vector3(0, yOffset, 0);
    }
}
