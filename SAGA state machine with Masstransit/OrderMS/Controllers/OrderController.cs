using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using OrderMS.Infra;
using OrderMS.Model;
using RabbitMq.Constants;
using RabbitMQ_Message;

namespace orderMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IOrderDataAccess _orderDataAccess;
        public OrderController(
          ISendEndpointProvider sendEndpointProvider, IOrderDataAccess orderDataAccess)
        {
            _sendEndpointProvider = sendEndpointProvider;
            _orderDataAccess = orderDataAccess;
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_orderDataAccess.GetAllOrder());
        }

        [HttpPost]
        [Route("createorder")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderModel orderModel)
        {
            _orderDataAccess.SaveOrder(orderModel);
            var endPoint = await _sendEndpointProvider.
                GetSendEndpoint(new Uri("queue:" + BusConstants.OrderQueue));

            await endPoint.Send<IOrderMessage>(new
            {
                OrderId = orderModel.OrderId,
                ProductName = orderModel.ProductName,
                PaymentCardNumber = orderModel.CardNumber
            });

            return Ok("Success");
        }

        [HttpPost]
        [Route("createorderstatemachine")]
        public async Task<IActionResult> CreateOrderUsingStateMachine([FromBody] OrderModel orderModel)
        {
            _orderDataAccess.SaveOrder(orderModel);
            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:" + BusConstants.SagaBusQueue));

            await endpoint.Send<IOrderStartEvent>(new
            {
                OrderId = orderModel.OrderId,
                PaymentCardNumber = orderModel.CardNumber,
                ProductName = orderModel.ProductName
            });

            return Ok("Success");
        }
        [HttpGet]
        [Route("getorder")]
        public async Task<IActionResult> GetOrder(Guid orderId)
        {
            try
            {
                return Ok(_orderDataAccess.GetOrder(orderId));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpDelete]
        [Route("deleteorder")]
        public async Task<IActionResult> DeleteOrder(Guid orderId)
        {
            try
            {
                return Ok(_orderDataAccess.DeleteOrder(orderId));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}