using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customer.Microservcie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerOrderController : ControllerBase
    {
        private readonly IBus _busService;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IBusControl _busControl;
        private readonly ISendEndpointProvider _SendEndpointProvider;
        public CustomerOrderController(IBus busService, 
            IPublishEndpoint publishEndpoint, 
            ISendEndpointProvider sendEndpointProvider, 
            IBusControl busControl)
        {
            _publishEndpoint = publishEndpoint;
            _SendEndpointProvider = sendEndpointProvider;
            _busControl = busControl;
            _busService = busService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder(CustomerOrder order)
        {
            await _busService.Publish(order);
            await _publishEndpoint.Publish<CustomerOrder>(order);
            return Ok();
            //if (order != null)
            //{
            //    order.OrderDate = DateTime.Now;
            //    Uri uri = new Uri("rabbitmq://localhost/orderQueue");
            //    var endPoint = await _busService.GetSendEndpoint(uri);
            //    await endPoint.Send(order);
            //    return "true";
            //}
            //return "false";
        }
        [HttpPost("Book")]
        public async Task<IActionResult> OrderBook(BookOrder order)
        {
            await _publishEndpoint.Publish<BookOrder>(order);
            return Ok();
            //if (order != null)
            //{
            //    Uri uri = new Uri("rabbitmq://localhost/orderExchange?bind=true&queue=orderQueue");
            //    var endPoint = await _busService.GetSendEndpoint(uri);
            //    await endPoint.Send(order);
            //    return Ok();
            //}
            //return BadRequest();
        }
    }
}