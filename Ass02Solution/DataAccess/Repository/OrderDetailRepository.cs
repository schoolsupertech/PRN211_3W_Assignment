using BusinessObject.Models;

namespace DataAccess.Repository;
public class OrderDetailRepository : IOrderDetailRepository {
    public void DeleteOrderDetail(int orderID, int productID) => OrderDetailDAO.Instance.Remove(orderID, productID);

    public IEnumerable<TblOrderDetail> GetOrderDetailListByListOrder(IEnumerable<TblOrder> id) => OrderDetailDAO.Instance.GetOrderDetailListByListOrder(id);

    public IEnumerable<TblOrderDetail> GetOrderDetails() => OrderDetailDAO.Instance.GetOrderDetailList();

    public TblOrderDetail? GetOrderDetailsById(int orderID, int productID) => OrderDetailDAO.Instance.GetOrderDetailById(orderID, productID);

    public IEnumerable<TblOrderDetail> GetOrderDetailsByMemberId(int id) => OrderDetailDAO.Instance.GetOrderDetailListByMemberId(id);

    public double GetStatistic(IEnumerable<TblOrder> order) => OrderDetailDAO.Instance.GetStatistic(order);

    public void InsertOrderDetail(TblOrderDetail orderDetail) => OrderDetailDAO.Instance.AddNew(orderDetail);

    public void UpdateOrderDetail(TblOrderDetail orderDetail) => OrderDetailDAO.Instance.Update(orderDetail);
}
