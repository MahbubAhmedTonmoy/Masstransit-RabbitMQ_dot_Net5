using System;

namespace RabbitMQ_Message
{
    public interface IOrderCancelEvent
    {
        public Guid OrderId { get; }
        public string PaymentCardNumber { get; }
        public string ProductName { get; }
    }
}
