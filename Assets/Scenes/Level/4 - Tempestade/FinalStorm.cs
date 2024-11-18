using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(Collider2D))]
public class FinalStorm : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _virtualCamera; // Referência à Cinemachine Virtual Camera
    [SerializeField] private CinemachineConfiner2D _confiner; // Referência ao componente Confiner2D
    [SerializeField] private Transform _startZoomPoint; // Ponto inicial do zoom (Transform)
    [SerializeField] private Transform _maxZoomPoint; // Ponto final do zoom (Transform)
    [SerializeField] private float _minZoom = 2f; // Zoom máximo (tamanho ortográfico mais baixo)
    [SerializeField] private float _maxZoom = 4f; // Zoom inicial (tamanho ortográfico maior)
    [SerializeField] private float _zoomSpeed = 5f; // Velocidade de interpolação do zoom

    private Transform player; // Referência ao Transform do jogador
    private bool isInArea = false;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform; // Busca o jogador pelo Tag
    }

    private void Update()
    {
        if(!isInArea)
            return;
        if (player.position.x <= _startZoomPoint.position.x && player.position.x >= _maxZoomPoint.position.x)
            CalculateCameraZoom();
    }

    private void CalculateCameraZoom(){
        // Calcula a progressão do jogador entre o ponto inicial e o ponto máximo
        float distance = Vector3.Distance(_startZoomPoint.position, _maxZoomPoint.position); // Distância total
        float playerProgress = Vector3.Distance(_startZoomPoint.position, player.position); // Distância do jogador ao ponto inicial

        // Calcula o progresso entre 0 e 1
        float t = Mathf.Clamp01(playerProgress / distance);

        // Interpola o FOV baseado no progresso
        float targetZoom = Mathf.Lerp(_maxZoom, _minZoom, t);
        _virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(_virtualCamera.m_Lens.OrthographicSize, targetZoom, Time.deltaTime * _zoomSpeed);


        // Invalida o cache do Confiner2D para reposicionar a câmera corretamente
        if (_confiner != null)
            _confiner.InvalidateCache();
    }

    public void ResetZoom()
    {
        // Caso queira reiniciar o zoom (por exemplo, ao sair da área)
        _virtualCamera.m_Lens.OrthographicSize = _maxZoom; // Volta ao FOV inicial
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInArea = true;
            Manager_Event.GameManager.OnForcedToCrouch.Get().Invoke(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInArea = false;
            Manager_Event.GameManager.OnForcedToCrouch.Get().Invoke(false);
            ResetZoom(); // Reseta o zoom da câmera
        }
    }
}
