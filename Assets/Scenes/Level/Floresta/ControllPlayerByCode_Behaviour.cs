using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[Serializable]
public class ControllPlayerByCode_Behaviour : PlayableBehaviour
{
    [SerializeField] private Vector2 _movement = Vector2.zero;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        var controller = playerData as ControllPlayerByCode;

        if(controller == null)
            return;

        controller.Movement = _movement;
    }
}
