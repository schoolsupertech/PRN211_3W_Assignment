using BusinessObject.Models;

namespace DataAccess.Repository; 
public interface IOrderDetailRepository {
    IEnumerable<TblOrderDetail> GetOrderDetails();
    IEnumerable<TblOrderDetail> GetOrderDetailsByMemberId(int id);
    TblOrderDetail GetOrderDetailsById(int orderID, int productID);
    void InsertOrderDetail(TblOrderDetail orderDetail);
    void UpdateOrderDetail(TblOrderDetail orderDetail);
    void DeleteOrderDetail(int orderID, int productID);
    public double GetStatistic(IEnumerable<TblOrder> order);
    public IEnumerable<TblOrderDetail> GetOrderDetailListByListOrder(IEnumerable<TblOrder> id);
}
