using System;
using System.Collections.Generic;
using UnityEngine;

namespace Slantar.Services
{
    public class EventProvider : IEventService
    {
        private class EventListener<T>
        {
            public int funcId;
            public GameObject target;
            public Action<T> callback;
        }

        private readonly Dictionary<Type, List<EventListener<object>>> eventCollection = new Dictionary<Type, List<EventListener<object>>>();
        private GameObject dummyObject;

        private GameObject GetDummyObject()
        {
            if(dummyObject == null)
            {
                dummyObject = new GameObject("Event service dummy");
                UnityEngine.Object.DontDestroyOnLoad(dummyObject);
            }

            return dummyObject;
        }

        private List<EventListener<object>> GetEventListeners(Type type)
        {
            List<EventListener<object>> eventListeners;
            if (!eventCollection.ContainsKey(type))
            {
                eventListeners = new List<EventListener<object>>();
                eventCollection.Add(type, eventListeners);
            }
            else
            {
                eventListeners = eventCollection[type];
            }

            return eventListeners;
        }

        public void Subscribe<T>(GameObject suscriberObject, Action<T> action)
        {
            Type type = typeof(T);
            GetEventListeners(type)
                .Add(new EventListener<object>()
                {
                    funcId = action.GetHashCode(),
                    callback = obj => action((T)obj),
                    target = suscriberObject == null ? GetDummyObject() : suscriberObject
                });
        }

        public void Subscribe<T>(Action<T> action)
        {
            Subscribe(GetDummyObject(), action);
        }

        public void Trigger<T>() where T : new()
        {
            Trigger(new T());
        }

        public void Trigger<T>(T eventData)
        {
            Type type = typeof(T);

            if (eventCollection.ContainsKey(type))
            {
                var currentActionList = eventCollection[type];

                EventListener<object> markedToRemove = null;

                for (int i = 0; i < currentActionList.Count; i++)
                {
                    var eventTarget = currentActionList[i];

                    if (eventTarget.target != null)
                    {
                        eventTarget.callback(eventData);
                    }
                    else
                    {
                        markedToRemove = eventTarget;
                    }
                }

                if(markedToRemove != null)
                {
                    currentActionList.Remove(markedToRemove);
                }
            }
        }

        public void Chain<T, Q>(GameObject subscriberObject) where Q : new()
        {
            if(subscriberObject != null)
            {
                Subscribe<T>(subscriberObject, (obj) => Trigger(new Q()));
            }
            else
            {
                Chain<T, Q>();
            }
        }

        public void Chain<T, Q>() where Q: new()
        {
            Chain<T, Q>(GetDummyObject());
        }

        public void Chain<T, Q>(Func<T, Q> Converter) where Q : new()
        {
            if(Converter != null)
            {
                Chain(GetDummyObject(), Converter);
            }
            else
            {
                Chain<T, Q>(GetDummyObject());
            }
        }

        public void Chain<T, Q>(GameObject suscriberObject, Func<T, Q> Converter) where Q : new()
        {
            if(Converter != null)
            {
                Subscribe<T>(suscriberObject, (obj) => Trigger(Converter((T)obj)));
            }
            else
            {
                Chain<T, Q>(suscriberObject);
            }
        }

        public bool Unsubscribe<T>(GameObject subscriberObject, Action<T> action)
        {
            Type type = typeof(T);

            if (eventCollection.ContainsKey(type))
            {
                var currentActionList = eventCollection[type];

                for (int i = 0; i < currentActionList.Count; i++)
                {
                    if(subscriberObject.GetInstanceID() == currentActionList[i].target.GetInstanceID() && currentActionList[i].funcId == action.GetHashCode())
                    {
                        currentActionList.RemoveAt(i);
                        return true;
                    }
                }
            }
            return false;
        }

        public bool Unsubscribe<T>(Action<T> action)
        {
            return Unsubscribe(GetDummyObject(), action);
        }
    }
}

