using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritController : MonoBehaviour
{
    [SerializeField] public Transform target;
    [SerializeField] private float followSpeed = 2f;

    private void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if (target == null)
            return;
            
        // Move suavemente o fantasma até a posição alvo usando Lerp
        transform.position = Vector2.Lerp(transform.position, target.position, followSpeed * Time.deltaTime);
    }
    
}
