using System;

namespace RabbitMQ_Message
{
    public interface IOrderStartEvent
    {
        public Guid OrderId { get; set; }
        public string PaymentCardNumber { get; set; }
        public string ProductName { get; set; }
    }
}
