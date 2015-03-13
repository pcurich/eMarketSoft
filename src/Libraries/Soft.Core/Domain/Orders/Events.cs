namespace Soft.Core.Domain.Orders
{
    /// <summary>
    /// Order paid event
    /// </summary>
    public class OrderPaidEvent
    {
        private readonly Order _order;

        public OrderPaidEvent(Order order)
        {
            _order = order;
        }

        public Order Order
        {
            get { return _order; }
        }
    }

    /// <summary>
    /// Order placed event
    /// </summary>
    public class OrderPlacedEvent
    {
        private readonly Order _order;

        public OrderPlacedEvent(Order order)
        {
            _order = order;
        }

        public Order Order
        {
            get { return _order; }
        }
    }

    /// <summary>
    /// Order cancelled event
    /// </summary>
    public class OrderCancelledEvent
    {
        private readonly Order _order;

        public OrderCancelledEvent(Order order)
        {
            _order = order;
        }

        public Order Order
        {
            get { return _order; }
        }
    }

    /// <summary>
    /// Order refunded event
    /// </summary>
    public class OrderRefundedEvent
    {
        private readonly Order _order;
        private readonly decimal _amount;

        public OrderRefundedEvent(Order order, decimal amount)
        {
            _order = order;
            _amount = amount;
        }

        public Order Order
        {
            get { return _order; }
        }

        public decimal Amount
        {
            get { return _amount; }
        }
    }
}