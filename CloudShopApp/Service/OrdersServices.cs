using CloudShopApp.Model;
using CloudShopApp.Model.Entity;
using System.Data;
using System.Text;

namespace CloudShopApp.Service
{
    public class OrdersServices : IOrderServices
    {
        private CloudShopDbContext context;

        public OrdersServices(CloudShopDbContext context)
        {
            this.context = context;
        }

        public Order AddOrder(Order order)
        {
            context.Orders.Add(order);
            context.SaveChanges();
            return order;
        }

        public List<Order> GetAllOrders()
        {
            return context.Orders.ToList();
        }

        public Order GetOrderById(int id)
        {
            return context.Orders.FirstOrDefault(order => order.Id == id);
        }

        public void RemoveOrderById(int id)
        {
            Order removeable = GetOrderById(id);
            if(removeable != null)
            {
                context.Orders.Remove(removeable);
            }
            context.SaveChanges();
        }

        public Order UpdateOrder(Order order)
        {
            var orderForChange = context.Orders.FirstOrDefault(_order => _order.Id == order.Id);
            if (orderForChange != null)
            {
                orderForChange.FeedbackContact = order.FeedbackContact;
                orderForChange.Description = order.Description;
                context.SaveChanges();
            }
            else
            {
                return null;
            }
            return orderForChange;
        }
    }
}
