using System;
using UnityEngine;

namespace Slantar.Services
{
    public delegate void SuscribeRequest(GameObject suscriberObject, Type messageType, Action<object> action);
    public delegate void TriggerRequest(Type messageType, object eventData);

    public interface IEventService
    {
        event SuscribeRequest OnSuscribeRequest;
        event TriggerRequest OnTriggerRequest;
        void Suscribe<T>(GameObject suscriberObject, Action<T> action);
        void Trigger<T>() where T : new();
        void Trigger<T>(T eventData);
        void Chain<T, Q>(GameObject suscriberObject, Func<T, Q> Converter) where Q : new();
    }
}