using System;
using System.Collections.Generic;
using UnityEngine;

namespace Slantar.Services
{
using EventCollection = System.Collections.Generic.Dictionary<System.Type, System.Collections.Generic.List<EventListener<object>>>;

    public class EventListener<T>
    {
	    public GameObject target;
	    public Action<T> callback;
    }

    public class SimpleEventController : MonoBehaviour
    {
	    private EventCollection eventCollection = new EventCollection();
	    private IEventService eventService;

	    private void Awake()
	    {
		    eventService = Core.Get<IEventService>();

		    eventService.OnSuscribeRequest += Suscribe;
		    eventService.OnTriggerRequest += Trigger;
	    }

	    private void OnDestroy()
	    {
		    eventService.OnSuscribeRequest -= Suscribe;
		    eventService.OnTriggerRequest -= Trigger;
	    }

	    private void Suscribe(GameObject suscriberObject, Type messageType, Action<object> action)
	    {
		    if (!eventCollection.ContainsKey(messageType))
		    {
			    var newEventListenerList = new List<EventListener<object>>();
			    newEventListenerList.Add(new EventListener<object>() { callback = action, target = suscriberObject });
			    eventCollection.Add(messageType, newEventListenerList);
		    }
		    else
		    {
			    var currentActionList = eventCollection[messageType];
			    currentActionList.Add(new EventListener<object>() { callback = action, target = suscriberObject });
		    }
	    }

	    private void Trigger(Type messageType, object eventData)
	    {

		    if (eventCollection.ContainsKey(messageType))
		    {
			    var currentActionList = eventCollection[messageType];

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

			    if (markedToRemove != null)
			    {
				    currentActionList.Remove(markedToRemove);
			    }
		    }
	    }
    }
}
