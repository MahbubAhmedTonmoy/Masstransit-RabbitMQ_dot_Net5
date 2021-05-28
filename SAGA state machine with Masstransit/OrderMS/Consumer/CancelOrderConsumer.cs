using MassTransit;
using OrderMS.Infra;
using RabbitMQ_Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderMS.Consumer
{
    public class CancelOrderConsumer :
     IConsumer<IOrderCancelEvent>
    {
        private readonly IOrderDataAccess _orderDataAccess;

        public CancelOrderConsumer(IOrderDataAccess orderDataAccess)
        {
            _orderDataAccess = orderDataAccess;
        }

        public async Task Consume(ConsumeContext<IOrderCancelEvent> context)
        {
            var data = context.Message;

            // delete from order database
            _orderDataAccess.DeleteOrder(data.OrderId);
        }
    }
}
