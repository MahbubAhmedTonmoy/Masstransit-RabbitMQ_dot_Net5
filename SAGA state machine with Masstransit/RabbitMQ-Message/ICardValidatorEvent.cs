using System;

namespace RabbitMQ_Message
{
    public interface ICardValidatorEvent
    {
        public Guid OrderId { get; }
        public string PaymentCardNumber { get; }
        public string ProductName { get; }
    }
}
