using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class Blockview : MonoBehaviour
{
    [SerializeField] private float _targetAlpha = 0f;
    private SpriteRenderer _sprite;
    private float _defaultAlpha;

    private void Start() {
        _sprite = GetComponent<SpriteRenderer>();
        _defaultAlpha = _sprite.color.a;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            _sprite.DOFade(_targetAlpha, 0.5f);
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            _sprite.DOFade(_defaultAlpha, 0.5f);
        }
    }
}
