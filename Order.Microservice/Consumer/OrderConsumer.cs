using MassTransit;
using Shared.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.Microservice.Consumer
{
    public class OrderConsumer : IConsumer<CustomerOrder>
    {
        public async Task Consume(ConsumeContext<CustomerOrder> context)
        {
            var obj = context.Message;
        }
    }

    public class Book : IConsumer<BookOrder>
    {
        public async Task Consume(ConsumeContext<BookOrder> context)
        {
            var obj = context.Message;
            Console.WriteLine("2");
        }
    }
}
