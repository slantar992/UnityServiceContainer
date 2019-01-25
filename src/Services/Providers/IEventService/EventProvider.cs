using System;
using UnityEngine;

namespace Slantar.Services
{
    public class EventProvider : IEventService
    {
        public event SuscribeRequest OnSuscribeRequest;
        public event TriggerRequest OnTriggerRequest;

        public void Suscribe<T>(GameObject suscriberObject, Action<T> action)
        {
            if (OnSuscribeRequest != null)
            {
                OnSuscribeRequest(suscriberObject, typeof(T), (obj) => { action((T)obj); });
            }
        }

        public void Trigger<T>() where T : new()
        {
            if (OnTriggerRequest != null)
            {
                OnTriggerRequest(typeof(T), new T());
            }
        }

        public void Trigger<T>(T eventData)
        {
            if (OnTriggerRequest != null)
            {
                OnTriggerRequest(typeof(T), eventData);
            }
        }

        public void Chain<T, Q>(GameObject suscriberObject, Func<T, Q> Converter) where Q : new()
        {
            if (Converter == null)
            {
                Suscribe<T>(suscriberObject, (obj) => Trigger(new Q()));
            }
            else
            {
                Suscribe<T>(suscriberObject, (obj) => Trigger(Converter((T)obj)));
            }
        }
    }
}

