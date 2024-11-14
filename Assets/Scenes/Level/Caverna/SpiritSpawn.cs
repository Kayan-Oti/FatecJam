using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SpiritSpawn : Dialogue_Trigger
{
    [SerializeField] private SpiritController _spirit;
    [SerializeField] private Transform _targetPosition;
    [SerializeField] private Transform _followPlayerTarget;
    [SerializeField] private AnimationCurve _moveEase;
    [SerializeField] private Collider2D _exitCollider2D;

    protected override void Start() {
        base.Start();
        _spirit.gameObject.SetActive(false);
        _exitCollider2D.enabled = false;
    }
    protected override void InteractionAction(){
        _spirit.gameObject.SetActive(true);
        SpriteRenderer sprite = _spirit.GetComponentInChildren<SpriteRenderer>();
        Color tmp = sprite.color;
        tmp.a = 0;
        sprite.color = tmp;

        Sequence animation = DOTween.Sequence();
        animation.Insert(0, sprite.DOFade(1f, 1f));
        animation.Insert(0, _spirit.transform.DOMove(_targetPosition.position, 1f)).SetEase(_moveEase);

        _extraEndAction = () => {
            EndInteraction();
            _spirit.target = _followPlayerTarget;
            _exitCollider2D.enabled = true;
        };

        animation.OnComplete(() => base.InteractionAction());
    }
}
