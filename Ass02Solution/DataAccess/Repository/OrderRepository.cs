using BusinessObject.Models;

namespace DataAccess.Repository;
public class OrderRepository : IOrderRepository {
    public void DeleteOrder(int id) => OrderDAO.Instance.Remove(id);

    public TblOrder? GetOrderById(int id) => OrderDAO.Instance.GetOrderById(id);

    public List<TblOrder> GetOrderByOrderDate(DateTime orderDate, DateTime dateT) => OrderDAO.Instance.Filter(orderDate, dateT);

    public IEnumerable<TblOrder> GetOrderListByMemberId(int id) => OrderDAO.Instance.GetOrderListByMemberId(id);

    public IEnumerable<TblOrder> GetOrders() => OrderDAO.Instance.GetOrderList();

    public void InsertOrder(TblOrder order) => OrderDAO.Instance.AddNew(order);

    public void UpdateOrder(TblOrder order) => OrderDAO.Instance.Update(order);
}
