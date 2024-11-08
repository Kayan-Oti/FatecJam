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
    }

    public class InteractionEvents{
        public class StartDialogueEvent: UnityEvent {}
        public GenericEvent<StartDialogueEvent> OnStartInteraction = new GenericEvent<StartDialogueEvent>();
        public class EndDialogueEvent: UnityEvent {}
        public GenericEvent<EndDialogueEvent> OnEndInteraction = new GenericEvent<EndDialogueEvent>();
    }
}
