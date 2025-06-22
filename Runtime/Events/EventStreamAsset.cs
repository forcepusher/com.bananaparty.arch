using System;
using System.Collections.Generic;
using UnityEngine;

namespace BananaParty.Registry
{
    public abstract class EventStreamAsset<TArgument> : ScriptableObject
    {
        [SerializeField]
        private bool _neverUnload = false;

        private readonly List<EventQueue<TArgument>> _eventQueues = new();

        private void OnEnable()
        {
            _eventQueues.Clear();

            if (_neverUnload)
                hideFlags |= HideFlags.DontUnloadUnusedAsset;
            else
                hideFlags &= ~HideFlags.DontUnloadUnusedAsset;
        }

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
