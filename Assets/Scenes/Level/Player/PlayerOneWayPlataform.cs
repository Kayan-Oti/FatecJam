using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneWayPlataform : MonoBehaviour
{
    private Collider2D _plataformCollider;
    private Collider2D _playerCollider;

    private void Start(){
        _playerCollider = GetComponent<Collider2D>();
    }

    private void Update() {
        if(_plataformCollider == null)
            return;

        if(PlayerInputManager.MOVEMENT.y < 0)
            StartCoroutine(DisableCollision());
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("OneWayPlataform"))
            _plataformCollider = other.gameObject.GetComponent<Collider2D>();
    }

    private IEnumerator DisableCollision(){
        Collider2D collider = _plataformCollider;
        _plataformCollider = null;
        
        Physics2D.IgnoreCollision(_playerCollider, collider);
        yield return new WaitForSeconds(0.25f);
        Physics2D.IgnoreCollision(_playerCollider, collider, false);
    }
}
