using System;
using System.Collections.Generic;

namespace BananaParty.Registry
{
    public class EventStream<TArgument>
    {
        private readonly List<EventQueue<TArgument>> _eventQueues = new();

        public void AddEvent(TArgument eventArgument)
        {
            foreach (EventQueue<TArgument> eventQueue in _eventQueues)
                eventQueue.AddEvent(eventArgument);
        }

        public EventQueue<TArgument> Subscribe()
        {
            var eventQueue = new EventQueue<TArgument>();
            _eventQueues.Add(eventQueue);
            return eventQueue;
        }

        public void Unsubscribe(EventQueue<TArgument> eventQueue)
        {
            if (eventQueue == null)
                throw new ArgumentNullException(nameof(eventQueue));

            _eventQueues.Remove(eventQueue);
        }
    }
}
