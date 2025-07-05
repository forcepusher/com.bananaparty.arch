using UnityEngine;

namespace BananaParty.Arch
{
    public abstract class EventHubAsset<TEventPayload> : ScriptableObject
    {
        [SerializeField]
        private bool _neverUnload = false;

        private readonly EventHub<TEventPayload> _eventHub = new();

        private void OnEnable()
        {
            _eventHub._eventQueues.Clear();

            if (_neverUnload)
                hideFlags |= HideFlags.DontUnloadUnusedAsset;
            else
                hideFlags &= ~HideFlags.DontUnloadUnusedAsset;
        }

        public void AddEvent(TEventPayload eventArgument)
        {
            _eventHub.AddEvent(eventArgument);
        }

        public EventQueue<TEventPayload> Subscribe()
        {
            return _eventHub.Subscribe();
        }

        public void Unsubscribe(EventQueue<TEventPayload> eventQueue)
        {
            _eventHub.Unsubscribe(eventQueue);
        }
    }
}
