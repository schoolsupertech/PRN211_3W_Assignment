using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IOrderRepository
    {

        List<OrderList> GetOrderList();
        void SaveOrderList(OrderDetail p, Order a);
        //List<Product> LoginMember(string p, string a);

        List<OrderList> SearchOrderdetails(int p);
        List<OrderList> SearchOrderdetailbyOrderID(int p);
        void UpdateOrderList(OrderDetail p, Order a);
        void DeleteOrderList(int id);

    }
}
