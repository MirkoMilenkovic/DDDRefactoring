namespace InvoiceWithDE.Common
{
    public interface IDomainEventHandler
    {
        void Handle(BaseDomainEvent de);

        DomainEventTypes EventType { get; }
    }
}
