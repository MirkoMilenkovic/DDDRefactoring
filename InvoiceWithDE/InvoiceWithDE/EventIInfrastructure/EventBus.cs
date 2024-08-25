using InvoiceWithDE.Common;
using System.Collections.ObjectModel;

namespace InvoiceWithDE.EventIInfrastructure
{
    public class EventBus
    {
        private readonly ILogger<EventBus> _logger;

        private readonly Dictionary<DomainEventTypes, List<IDomainEventHandler>> _handlers = new();

        public EventBus(ILogger<EventBus> logger)
        {
            _logger = logger;
        }


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

            _logger.LogInformation($"{handler.GetType().Name} added for {handler.EventType}");
        }

        public void Handle(BaseDomainEvent ev)
        {
            if (!_handlers.TryGetValue(ev.EventType, out List<IDomainEventHandler>? handlersForType))
            {
                // no handler

                _logger.LogInformation($"No handlers for {ev.EventType}");
                
                return;
            }

            foreach (IDomainEventHandler handler in handlersForType)
            {
                _logger.LogInformation($"Executing event handler: {handler.GetType().Name} for {ev.EventType}");

                handler.Handle(ev);

                _logger.LogInformation($"Executed event handler: {handler.GetType().Name} for {ev.EventType}");
            }
        }
    }
}
