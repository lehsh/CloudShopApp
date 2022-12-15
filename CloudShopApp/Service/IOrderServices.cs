using CloudShopApp.Migrations;
using CloudShopApp.Model.Entity;

namespace CloudShopApp.Service
{
    public interface IOrderServices
    {
        List<Order> GetAllOrders();
        Order GetOrderById(int id);
        Order AddOrder(Order order);
        void RemoveOrderById(int id);
        Order UpdateOrder(Order order);
    }
}
