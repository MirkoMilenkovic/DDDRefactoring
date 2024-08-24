using System.Text.Json;

namespace InvoiceWithDE.Common
{
    public abstract class BaseDomainEvent
    {
        public abstract DomainEventTypes EventType { get; }

        public abstract string Serialize();
    }

    public abstract class BaseDomainEvent<TPayload> : BaseDomainEvent
    {
        protected BaseDomainEvent(
            TPayload payload)
        {
            Payload = payload;
        }

        public TPayload Payload { get; }

        /// <summary>
        /// Default implementation of serialization using System.Text.Json
        /// </summary>
        public override string Serialize()
        {
            string payloadSerialized = JsonSerializer.Serialize(this.Payload);

            return payloadSerialized;
        }
    }
}