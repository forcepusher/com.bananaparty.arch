using System.Collections.Generic;

namespace BananaParty.Arch
{
    public class EventQueue<TEventPayload>
    {
        private readonly Queue<TEventPayload> _eventQueue = new();

        public bool HasUnreadEvents => _eventQueue.Count > 0;

        public void AddEvent(TEventPayload eventArgument)
        {
            _eventQueue.Enqueue(eventArgument);
        }

        public TEventPayload Read()
        {
            return _eventQueue.Dequeue();
        }
    }
}
