using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;

public class PlayerOneWayPlataform : MonoBehaviour
{
    [SerializeField] [ReadOnly] private List<Collider2D> _plataformsColliders;
    [SerializeField] [ReadOnly] private Collider2D _playerCollider;

    private void Start(){
        _playerCollider = GetComponent<Collider2D>();
    }

    private void Update() {
        if(_plataformsColliders.Count == 0)
            return;

        if(PlayerInputManager.MOVEMENT.y < 0){
            foreach(Collider2D collider in _plataformsColliders){
                StartCoroutine(DisableCollision(collider));
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("OneWayPlataform"))
            _plataformsColliders.Add(other.gameObject.GetComponent<Collider2D>());
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.CompareTag("OneWayPlataform"))
            _plataformsColliders.Remove(other.gameObject.GetComponent<Collider2D>());
    }

    private IEnumerator DisableCollision(Collider2D collider){
        Physics2D.IgnoreCollision(_playerCollider, collider);
        yield return new WaitForSeconds(0.25f);
        Physics2D.IgnoreCollision(_playerCollider, collider, false);
    }
}
