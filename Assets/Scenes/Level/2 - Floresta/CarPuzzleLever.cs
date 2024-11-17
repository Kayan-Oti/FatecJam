using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPuzzleLever : Abstract_Interactable
{
    [SerializeField] private Collider2D _carPlataform;
    [SerializeField] private Transform _capoPivot;
    [SerializeField] private TutorialInteract _crouchTutorial;
    private bool _hasMoreInteraciton = true;

    protected override void Start() {
        base.Start();
        _carPlataform.enabled = false;
    }

    protected override void InteractionAction()
    {
        _carPlataform.enabled = true;
        _capoPivot.transform.eulerAngles = new Vector3(0, 0, 37);
        _hasMoreInteraciton = false;

        _crouchTutorial.DisableTutorial();
        EndInteraction();
    }

    protected override bool HasMoreInteraction()
    {
        return _hasMoreInteraciton;
    }
}
