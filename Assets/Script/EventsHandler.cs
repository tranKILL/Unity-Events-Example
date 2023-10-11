namespace NJ_Event
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [DisallowMultipleComponent]

    public class EventsHandler : MonoBehaviour
    {
        private Dictionary<string, Action<Dictionary<string, object>>> eventDictionary;
        private static EventsHandler eventManager;

        public static EventsHandler instance
        {
            get
            {
                if (!eventManager)
                {
                    eventManager = FindObjectOfType(typeof(EventsHandler)) as EventsHandler;

                    if (!eventManager)
                    {
                        Debug.LogError("There needs to be one active EventManager script on a GameObject in your scene.");
                    }
                    else
                    {
                        eventManager.Init();

                        //  Sets this to not be destroyed when reloading scene
                        DontDestroyOnLoad(eventManager);
                    }
                }
                return eventManager;
            }
        }

        void Init()
        {
            if (eventDictionary == null)
            {
                eventDictionary = new Dictionary<string, Action<Dictionary<string, object>>>();
            }
        }

        public static void StartListening(string eventName, Action<Dictionary<string, object>> listener)
        {
            Action<Dictionary<string, object>> thisEvent;

            if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent += listener;
                instance.eventDictionary[eventName] = thisEvent;
            }
            else
            {
                thisEvent += listener;
                instance.eventDictionary.Add(eventName, thisEvent);
            }
        }

        public static void StopListening(string eventName, Action<Dictionary<string, object>> listener)
        {
            if (eventManager == null) return;
            Action<Dictionary<string, object>> thisEvent;
            if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent -= listener;
                instance.eventDictionary[eventName] = thisEvent;
            }
        }

        public static void TriggerEvent(string eventName, Dictionary<string, object> message)
        {
Debug.Log("2 Event: " + eventName);
            Action<Dictionary<string, object>> thisEvent = null;
            if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.Invoke(message);
            }
        }
    }
}