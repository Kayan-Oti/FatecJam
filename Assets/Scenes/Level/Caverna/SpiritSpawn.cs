using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SpiritSpawn : Dialogue_Trigger
{
    [SerializeField] private GameObject _spirit;
    [SerializeField] private Transform _targetPosition;
    [SerializeField] private AnimationCurve _moveEase;
    [SerializeField] private Collider2D _exitCollider2D;

    protected override void Start() {
        base.Start();
        _spirit.SetActive(false);
        _exitCollider2D.enabled = false;
    }
    protected override void InteractionAction(){
        _spirit.SetActive(true);
        SpriteRenderer sprite = _spirit.GetComponentInChildren<SpriteRenderer>();
        Color tmp = sprite.color;
        tmp.a = 0;
        sprite.color = tmp;

        Sequence animation = DOTween.Sequence();
        animation.Insert(0, sprite.DOFade(1f, 1f));
        animation.Insert(0, _spirit.transform.DOMove(_targetPosition.position, 1f)).SetEase(_moveEase);

        animation.OnComplete(() => {
           base.InteractionAction();
           _spirit.GetComponent<SpiritController>().target = _targetPosition;
           _exitCollider2D.enabled = true;
        });
    }
}
