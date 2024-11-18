using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class Manager_Event
{
    public static readonly GameEvents GameManager = new GameEvents();
    public static readonly InteractionEvents InteractionManager = new InteractionEvents();

    public class GenericEvent<T> where T: class, new()
    {
        private Dictionary<string, T> map = new Dictionary<string, T>();

        public T Get(string channel = ""){
            map.TryAdd(channel, new T());
            return map[channel];
        }
    }

    public class GameEvents{
        public class ChangingScene: UnityEvent {}
        public GenericEvent<ChangingScene> OnChanginScene = new GenericEvent<ChangingScene>();
        public class LoadedScene: UnityEvent {}
        public GenericEvent<LoadedScene> OnLoadedScene = new GenericEvent<LoadedScene>();
        public class ChangeCurrentSelectedUI: UnityEvent<GameObject> {}
        public GenericEvent<ChangeCurrentSelectedUI> OnChangeCurrentSelectedUI = new GenericEvent<ChangeCurrentSelectedUI>();

        public class ForcedToCrouch: UnityEvent<bool> {}
        public GenericEvent<ForcedToCrouch> OnForcedToCrouch = new GenericEvent<ForcedToCrouch>();
    }

    public class InteractionEvents{
        public class StartInteractionEvent: UnityEvent {}
        public GenericEvent<StartInteractionEvent> OnStartInteraction = new GenericEvent<StartInteractionEvent>();
        public class EndInteractionEvent: UnityEvent {}
        public GenericEvent<EndInteractionEvent> OnEndInteraction = new GenericEvent<EndInteractionEvent>();

        public class StartTimelineEvent: UnityEvent {}
        public GenericEvent<StartTimelineEvent> OnStartTimeline = new GenericEvent<StartTimelineEvent>();
        public class EndTimelineEvent: UnityEvent {}
        public GenericEvent<EndTimelineEvent> OnEndTimeline = new GenericEvent<EndTimelineEvent>();
    }
}
