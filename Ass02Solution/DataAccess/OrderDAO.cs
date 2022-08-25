using BusinessObject.Models;

namespace DataAccess; 
public class OrderDAO {
    private static OrderDAO instance;
    private static readonly object instanceLock = new object();
    private OrderDAO() { }
    public static OrderDAO Instance {
        get {
            lock(instanceLock) {
                if(instance == null) {
                    instance = new OrderDAO();
                }
                return instance;
            }
        }
    }

    public IEnumerable<TblOrder> GetOrderList() {
        List<TblOrder> orders;
        try {
            using SalesManagementContext context = new SalesManagementContext();
            orders = context.TblOrders.ToList();
        }
        catch(Exception ex) {
            throw new Exception(ex.Message);
        }
        return orders;
    }

    public IEnumerable<TblOrder> GetOrderListByMemberId(int memberId) {
        var orders = GetOrderList();
        List<TblOrder> fil = new List<TblOrder>();

        try {
            foreach(TblOrder order in orders) {
                if(order.MemberId == memberId) {
                    fil.Add(order);
                }
            }
        }
        catch(Exception ex) {
            throw new Exception(ex.Message);
        }
        return fil;
    }

    public TblOrder? GetOrderById(int OrderId) {
        TblOrder? order = null;
        try {
            using SalesManagementContext context = new SalesManagementContext();
            order = context.TblOrders.SingleOrDefault(o => o.OrderId == OrderId);
        }
        catch(Exception ex) {
            throw new Exception(ex.Message);
        }
        return order;
    }

    public void AddNew(TblOrder order) {
        try {
            TblOrder? o = GetOrderById(order.OrderId);
            if (o == null) {
                using SalesManagementContext context = new SalesManagementContext();
                context.TblOrders.Add(order);
                context.SaveChanges();
            }
            else {
                throw new Exception("Error: The order ID is already exists");
            }
        }
        catch (Exception ex) {
            throw new Exception(ex.Message);
        }
    }

    public void Update(TblOrder order) {
        try {
            TblOrder? o = GetOrderById(order.OrderId);
            if(o != null) {
                using SalesManagementContext context = new SalesManagementContext();
                context.TblOrders.Update(order);
                context.SaveChanges();
            }
            else {
                throw new Exception("Error: The Order ID doesn't exists.");
            }
        }
        catch(Exception ex) {
            throw new Exception(ex.Message);
        }
    }

    public void Remove(int orderID) {
        try {
            TblOrder? order = GetOrderById(orderID);
            if(order != null) {
                using SalesManagementContext context = new SalesManagementContext();
                context.TblOrders.Remove(order);
            }
        }
        catch(Exception ex) {
            throw new Exception(ex.Message);
        }
    }

    public List<TblOrder> Filter (DateTime a, DateTime b) {
        var orders = GetOrderList();
        List<TblOrder> fil = new List<TblOrder>();

        try {
            foreach(var order in orders) {
                if(order.OrderDate >= a && order.OrderDate <= b) {
                    fil.Add(order);
                }
            }
            fil = fil.OrderByDescending(x => x.OrderDate).ToList();
        }
        catch(Exception ex) {
            throw new Exception(ex.Message);
        }

        return fil;
    }
}
