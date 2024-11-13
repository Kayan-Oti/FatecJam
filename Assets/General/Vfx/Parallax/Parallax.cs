using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private Vector3 _lastCameraPosition;
    [SerializeField] private Vector2 _parallaxSpeed = new Vector2 (0.5f, 0.5f);
    private float _textureUnitSizeX;
    private float CameraDistanceX => _cameraTransform.position.x - transform.position.x;

    private void Start() {
        _cameraTransform = Camera.main.transform;
        _lastCameraPosition = _cameraTransform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        _textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
    }


    private void Update () {
        Vector3 deltaMovement = _cameraTransform.position - _lastCameraPosition;
        transform.position += new Vector3(deltaMovement.x * _parallaxSpeed.x, deltaMovement.y * _parallaxSpeed.y);
        _lastCameraPosition = _cameraTransform.position;

        if(Mathf.Abs(CameraDistanceX) >= _textureUnitSizeX){
            float offsetPostionX = CameraDistanceX % _textureUnitSizeX;
            transform.position = new Vector3(_cameraTransform.position.x + offsetPostionX, transform.position.y);
        }
    }
}
