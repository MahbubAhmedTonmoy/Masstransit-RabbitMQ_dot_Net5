using Automatonymous;
using RabbitMQ_Message;

namespace Saga_StateMachine
{
    public class OrderStateMachine : MassTransitStateMachine<OrderStateData>
    {
        public State Validation { get; private set; }
        public Event<IOrderStartEvent> @startOrderevent { get; private set; }
        public Event<IOrderCancelEvent> @cancelOrderevent { get; private set; }

        public OrderStateMachine()
        {
            InstanceState(s => s.CurrentState);

            Event(() => @startOrderevent, x => x.CorrelateById(m => m.Message.OrderId));
            Event(() => @cancelOrderevent, x => x.CorrelateById(x => x.Message.OrderId));

            Initially(
                When(@startOrderevent)
                .Then(context =>
                {
                    context.Instance.OrderId = context.Data.OrderId;
                    context.Instance.PaymentCardNumber = context.Data.PaymentCardNumber;
                    context.Instance.ProductName = context.Data.ProductName;
                })
                .TransitionTo(Validation)
                .Publish(context => new CardValidateEvent(context.Instance))
                .Finalize()
              );

            During(Validation,
                When(@cancelOrderevent)
                .Then(context =>
                {
                    context.Instance.OrderId = context.Data.OrderId;
                })
                .Finalize()
               );

            SetCompletedWhenFinalized();   
        }
    }
}
