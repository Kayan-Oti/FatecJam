using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class ControllPlayerByCode_Clip : PlayableAsset, ITimelineClipAsset
{
    [SerializeField]
    private ControllPlayerByCode_Behaviour template = new ControllPlayerByCode_Behaviour();

    public ClipCaps clipCaps => ClipCaps.None;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        return ScriptPlayable<ControllPlayerByCode_Behaviour>.Create(graph, template);
    }
}
