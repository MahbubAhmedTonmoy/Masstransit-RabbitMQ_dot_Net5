using MassTransit;
using RabbitMQ_Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMS.Consumer
{
    public class OrderCardNumberValidateConsumer :
      IConsumer<IOrderMessage>
    {
        public async Task Consume(ConsumeContext<IOrderMessage> context)
        {
            var data = context.Message;
            if (data.PaymentCardNumber != "test")
            {
                // invalid
            }
        }
    }

    public class OrderValidateConsumer : IConsumer<ICardValidatorEvent>
    {
        public async Task Consume(ConsumeContext<ICardValidatorEvent> context)
        {
            var data = context.Message;
            if(data.PaymentCardNumber.Contains("string"))
            {
                await context.Publish<IOrderCancelEvent>(new 
                { OrderId = context.Message.OrderId, 
                    PaymentCardNumber = context.Message.PaymentCardNumber });
            }
        }
    }
}
