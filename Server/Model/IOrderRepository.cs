namespace hmw
{
    public interface IOrderRepository
    { 
        List<Order> GetAllOrders();
        Order GetOrderByName(string name);
        void AddOrder(Order order);
        void DeleteOrder(string id);
    }
}