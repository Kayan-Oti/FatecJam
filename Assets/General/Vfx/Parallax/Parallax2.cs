using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax2 : MonoBehaviour
{
    [Range(0,1)]
    [SerializeField] private float _parallaxEffect;
    [SerializeField] private bool _infinity = true;
    private float _length, _startpos;
    private Transform _camPos;

    void Start()
    {
        _camPos = Camera.main.transform;
        _startpos = transform.position.x;
        _length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
        float temp = _camPos.transform.position.x * (1 - _parallaxEffect);
        float dist = _camPos.transform.position.x * _parallaxEffect;

        transform.position = new Vector3(_startpos + dist, transform.position.y, transform.position.z);

        //Infinity Background
        if(!_infinity)
            return;
        if (temp > _startpos + _length) _startpos += _length;
        else if (temp < _startpos - _length) _startpos -= _length;
    }
}
