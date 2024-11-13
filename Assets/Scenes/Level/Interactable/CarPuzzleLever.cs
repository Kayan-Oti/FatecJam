using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPuzzleLever : Abstract_Interactable
{
    [SerializeField] private GameObject _carPlataform;
    private bool _hasMoreInteraciton = true;

    protected override void Start() {
        base.Start();
        _carPlataform.SetActive(false);
    }

    protected override void InteractionAction()
    {
        _carPlataform.SetActive(true);
        _hasMoreInteraciton = false;
        EndInteraction();
    }

    protected override bool HasMoreInteraction()
    {
        return _hasMoreInteraciton;
    }
}
