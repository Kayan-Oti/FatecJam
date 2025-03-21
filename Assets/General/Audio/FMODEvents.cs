using UnityEngine;
using FMODUnity;
using MyBox;

public class FMODEvents : Singleton<FMODEvents>
{
    // [field: Header("Temp")]
    // [field: SerializeField] public EventReference temp { get; private set;}

    [field: Header("Music")]
    [field: SerializeField] public EventReference MenuMusic { get; private set;}
    [field: SerializeField] public EventReference CavernaMusic { get; private set;}
    [field: SerializeField] public EventReference FlorestaMusic { get; private set;}
    [field: SerializeField] public EventReference CidadeMusic { get; private set;}
    [field: SerializeField] public EventReference TempestadeMusic { get; private set;}
    [field: SerializeField] public EventReference FinalMusic { get; private set;}

    [field: Header("Ambience")]
    [field: SerializeField] public EventReference WindAmbience { get; private set;}
    [field: SerializeField] public EventReference StrongWind { get; private set;}

    [field: Header("UI")]
    [field: SerializeField] public EventReference ButtonHover { get; private set;}
    [field: SerializeField] public EventReference ButtonClick { get; private set;}
    [field: SerializeField] public EventReference ButtonPlay { get; private set;}

    [field: Header("LoadingScreen")]
    [field: SerializeField] public EventReference LoadingScreenStart { get; private set;}
    [field: SerializeField] public EventReference LoadingScreenEnd { get; private set;}

    [field: Header("Player")]
    [field: SerializeField] public EventReference FootSteps { get; private set;}
    [field: SerializeField] public EventReference OnGrounded { get; private set;}

    [field: Header("Misc")]
    [field: SerializeField] public EventReference CarOpen { get; private set;}
    [field: SerializeField] public EventReference BoneDrop { get; private set;}
}
