using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class StrongWind : MonoBehaviour
{
    [SerializeField] private Vector2 windForce = new Vector2(5f, 0f); // Direção e força do vento (esquerda neste caso)
    [SerializeField] private bool isGust = false; // O vento é uma rajada intermitente?
    [SerializeField] private float gustInterval = 2f; // Intervalo entre as rajadas
    [SerializeField] private float gustDuration = 1f; // Duração de cada rajada

    private bool isActive = true;

    private void Start()
    {
        if (isGust)
        {
            StartCoroutine(GustController());
        }
    }

    private IEnumerator GustController()
    {
        while (true)
        {
            isActive = true;
            yield return new WaitForSeconds(gustDuration);
            isActive = false;
            yield return new WaitForSeconds(gustInterval);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Verifica se o objeto tem um Rigidbody2D e aplica a força do vento
        if (isActive && collision.TryGetComponent(out Rigidbody2D rb))
        {
            rb.AddForce(windForce);
        }
    }
}
