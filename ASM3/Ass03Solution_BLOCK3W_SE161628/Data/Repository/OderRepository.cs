using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class OderRepository : IOrderRepository
    {
        public void DeleteOrderList(int id)
        {
           OrderDAO.DeleteOrder(id);
        }

        public Order GetListByID(int p) => OrderDAO.GetListByID(p);
        

        public List<OrderList> GetOrderList() => OrderDAO.GetOrderList();
        public List<OrderList> SearchOrderdetails(int p)=> OrderDAO.SearchOrderdetails(p);

        public void SaveOrderList(OrderDetail p, Order a)
        {
            OrderDAO.SaveOrderList( p, a);
        }

        public void UpdateOrderList(OrderDetail p, Order a)
        {
            OrderDAO.UpdateOrder(p,a);
        }

        public List<OrderList> SearchOrderdetailbyOrderID(int p) => OrderDAO.SearchOrderdetailbyOrderID(p);

    }
}
