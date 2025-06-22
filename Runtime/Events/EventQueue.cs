using System.Collections.Generic;

namespace BananaParty.Registry
{
    public class EventQueue<TArgument>
    {
        private readonly Queue<TArgument> _eventQueue = new();

        public bool HasUnreadEvents => _eventQueue.Count > 0;

        public void AddEvent(TArgument eventArgument)
        {
            _eventQueue.Enqueue(eventArgument);
        }

        public TArgument Read()
        {
            return _eventQueue.Dequeue();
        }
    }
}
