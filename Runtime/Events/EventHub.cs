using System;
using System.Collections.Generic;

namespace BananaParty.Arch
{
    public class EventHub<TEventPayload>
    {
        internal readonly List<EventQueue<TEventPayload>> _eventQueues = new();

        public void AddEvent(TEventPayload eventArgument)
        {
            foreach (EventQueue<TEventPayload> eventQueue in _eventQueues)
                eventQueue.AddEvent(eventArgument);
        }

        public EventQueue<TEventPayload> Subscribe()
        {
            var eventQueue = new EventQueue<TEventPayload>();
            _eventQueues.Add(eventQueue);
            return eventQueue;
        }

        public void Unsubscribe(EventQueue<TEventPayload> eventQueue)
        {
            if (eventQueue == null)
                throw new ArgumentNullException(nameof(eventQueue));

            _eventQueues.Remove(eventQueue);
        }
    }
}
