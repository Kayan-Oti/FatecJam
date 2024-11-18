using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ExitLevel : MonoBehaviour
{
    [SerializeField] private SceneIndex _nextLevel;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")){
            //Stop Song
            AudioManager.Instance.StopMusic();
            //Next Level
            GameManager.Instance.LoadScene(_nextLevel);
        }
    }
}
