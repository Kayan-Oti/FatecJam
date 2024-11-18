using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpiritAnimation : MonoBehaviour
{
    [SerializeField] private float _hoverAmount = 0.5f; // Quantidade de movimento para cima e para baixo
    [SerializeField] private float _hoverSpeed = 2f; // Duração do movimento de subida/descida
    private Vector3 initialPosition;
    private SpriteRenderer _targetFlip;
    private SpriteRenderer _sprite;
    public bool _canFlip = true;

    private void Start() {
        initialPosition = transform.localPosition;
        _sprite = GetComponent<SpriteRenderer>();
        _targetFlip = FindObjectOfType<PlayerAnimator>().gameObject.GetComponentInChildren<SpriteRenderer>();
    }
    private void Update()
    {
        // Inicia o movimento de subida e descida (efeito hover) com DOTween
        HoverEffect();
        FlipSprite();
    }
    private void HoverEffect()
    {
        // Aplica uma flutuação no eixo Y usando uma função senoidal para o fantasma (filho)
        float yOffset = Mathf.Sin(Time.time * _hoverSpeed) * _hoverAmount;
        transform.localPosition = initialPosition + new Vector3(0, yOffset, 0);
    }

    private void FlipSprite(){
        if(!_canFlip)
            return;
        //Flip sprite
        if(_sprite.flipX != _targetFlip.flipX)
            _sprite.flipX = _targetFlip.flipX;
    }
}
