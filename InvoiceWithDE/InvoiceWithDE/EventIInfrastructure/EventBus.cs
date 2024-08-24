using InvoiceWithDE.Common;

namespace InvoiceWithDE.EventIInfrastructure
{
    public class EventBus
    {
        Dictionary<DomainEventTypes, List<IDomainEventHandler>> _handlers = new();


        /// <summary>
        /// Thread-unsafe!!!!
        /// </summary>
        public void Subscribe(
            IDomainEventHandler handler)
        {
            if (_handlers.TryGetValue(handler.EventType, out List<IDomainEventHandler>? handlersForType))
            {
                handlersForType.Add(handler);
            }
            else
            {
                _handlers.Add(
                    handler.EventType,
                    new List<IDomainEventHandler>() { handler });
            }
        }

        public void Handle(BaseDomainEvent ev)
        {
            if (!_handlers.TryGetValue(ev.EventType, out List<IDomainEventHandler>? handlersForType))
            {
                // no handler
                return;
            }

            foreach (IDomainEventHandler handler in handlersForType)
            {
                handler.Handle(ev);
            }
        }
    }
}
