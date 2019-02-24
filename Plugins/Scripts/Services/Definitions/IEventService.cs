using System;
using UnityEngine;

namespace Slantar.Services
{
    public interface IEventService
    {
        /// <summary>
        /// Subscribes a method from gameobject for T event
        /// </summary>
        /// <typeparam name="T"> event type</typeparam>
        /// <param name="subscriberObject"> gameObject to subscribe </param>
        /// <param name="action"> method to execute </param>
        void Subscribe<T>(GameObject subscriberObject, Action<T> action);

        /// <summary>
        /// Subscribes a method for T event
        /// </summary>
        /// <typeparam name="T"> event type</typeparam>
        /// <param name="action"> method to execute </param>
        void Subscribe<T>(Action<T> action);
        
        /// <summary>
        /// Triggers the event T with default constructor
        /// </summary>
        /// <typeparam name="T">event type</typeparam>
        void Trigger<T>() where T : new();

        /// <summary>
        /// Triggers the event T
        /// </summary>
        /// <typeparam name="T">event Type</typeparam>
        /// <param name="eventData">event data</param>
        void Trigger<T>(T eventData);

        /// <summary>
        /// Subscribes an event that returns another event that it will trigger it after (Event groups)
        /// </summary>
        /// <typeparam name="T">first event type</typeparam>
        /// <typeparam name="Q">second event type that triggers T after</typeparam>
        /// <param name="suscriberObject">gameObject to subscribe</param>
        void Chain<T, Q>(GameObject suscriberObject) where Q : new();

        /// <summary>
        /// Subscribes a method event that returns another event that it will trigger it after (Event groups)
        /// </summary>
        /// <typeparam name="T">first event type</typeparam>
        /// <typeparam name="Q">second event type that triggers T after</typeparam>
        /// <param name="subscriberObject">gameObject to subscribe</param>
        /// <param name="Converter">Function that transforms event T to event Q</param>
        void Chain<T, Q>(GameObject subscriberObject, Func<T, Q> Converter) where Q : new();

        /// <summary>
        /// Subscribes a method event that returns another event that it will trigger it after (Event groups)
        /// </summary>
        /// <typeparam name="T">first event type</typeparam>
        /// <typeparam name="Q">second event type that triggers T after</typeparam>
        /// <param name="Converter"></param>
        void Chain<T, Q>(Func<T, Q> Converter) where Q : new();
        
        /// <summary>
        /// Subscribes a method event that returns another event that it will trigger it after (Event groups)
        /// </summary>
        /// <typeparam name="T">first event type</typeparam>
        /// <typeparam name="Q">second event type that triggers T after</typeparam>
        void Chain<T, Q>() where Q : new();

        /// <summary>
        /// Unsubscribe the event T method from subscriberObject 
        /// </summary>
        /// <typeparam name="T">first event type</typeparam>
        /// <param name="subscriberObject">gameObject to subscribe</param>
        /// <param name="Converter">Function that transforms event T to event Q</param>
        /// <returns>returns if the event is removed succesfully</returns>
        bool Unsubscribe<T>(GameObject subscriberObject, Action<T> action);

        /// <summary>
        /// Unsubscribe the event T method
        /// </summary>
        /// <typeparam name="T">first event type</typeparam>
        /// <param name="subscriberObject">gameObject to subscribe</param>
        /// <param name="Converter">Function that transforms event T to event Q</param>
        /// <returns>returns if the event is removed succesfully</returns>
        bool Unsubscribe<T>(Action<T> action);
    }
}