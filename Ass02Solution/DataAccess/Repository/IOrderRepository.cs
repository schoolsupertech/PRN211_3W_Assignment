using BusinessObject.Models;

namespace DataAccess.Repository; 
public interface IOrderRepository {
    IEnumerable<TblOrder> GetOrders();
    IEnumerable<TblOrder> GetOrderListByMemberId(int id);
    TblOrder GetOrderById(int id);
    void InsertOrder(TblOrder order);
    void UpdateOrder(TblOrder order);
    void DeleteOrder(int id);
    List<TblOrder> GetOrderByOrderDate(DateTime orderDate, DateTime dateT);
}
