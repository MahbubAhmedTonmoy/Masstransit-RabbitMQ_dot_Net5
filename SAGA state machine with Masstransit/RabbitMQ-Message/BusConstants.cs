using System;

namespace RabbitMq.Constants
{
    public class BusConstants
    {
        public const string RabbitMqUri = "rabbitmq://localhost/";
        public const string UserName = "guest";
        public const string Password = "guest";
        public const string OrderQueue = "validate-order-queue";
        public const string SagaBusQueue = "SagaBusQueue";
    }
}
