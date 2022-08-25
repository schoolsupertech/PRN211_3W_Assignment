using BusinessObject.Models;

namespace DataAccess; 
public class OrderDetailDAO {
    private static OrderDetailDAO instance;
    private static readonly object instanceLock = new object();
    private OrderDetailDAO() { }
    public static OrderDetailDAO Instance {
        get {
            lock(instanceLock) {
                if(instance == null) {
                    instance = new OrderDetailDAO();
                }
                return instance;
            }
        }
    }

    public IEnumerable<TblOrderDetail> GetOrderDetailList() {
        List<TblOrderDetail> orderDetail;
        try {
            using SalesManagementContext context = new SalesManagementContext();
            orderDetail = context.TblOrderDetails.ToList();
        }
        catch(Exception ex) {
            throw new Exception(ex.Message);
        }
        return orderDetail;
    }

    public IEnumerable<TblOrderDetail> GetOrderDetailListByMemberId(int memberId) {
        var orderDetails = GetOrderDetailList().ToList();
        var orders = OrderDAO.Instance.GetOrderList();
        List<TblOrder> fil = new List<TblOrder>();
        List<TblOrderDetail> final = new List<TblOrderDetail>();

        try {
            foreach(var order in orders) {
                if(order.MemberId == memberId) {
                    fil.Add(order);
                }
            }
            for(int i = 0; i < fil.Count(); i++) {
                for(int j = 0; j < orderDetails.Count(); j++) {
                    if (fil[i].OrderId == orderDetails[j].OrderId) {
                        final.Add(orderDetails[j]);
                    }
                }
            }
        }
        catch(Exception ex) {
            throw new Exception(ex.Message);
        }

        return final;
    }

    public IEnumerable<TblOrderDetail> GetOrderDetailListByListOrder(IEnumerable<TblOrder> orders) {
        var orderDetails = GetOrderDetailList();
        List<TblOrderDetail> final = new List<TblOrderDetail>();

        try {
            foreach(var order in orders) {
                foreach(var orderDetail in orderDetails) {
                    if(order.OrderId == orderDetail.OrderId) {
                        final.Add(orderDetail);
                    }
                }
            }
        }
        catch(Exception ex) {
            throw new Exception(ex.Message);
        }

        return final;
    }

    public double GetStatistic(IEnumerable<TblOrder> order) {
        double total = 0;
        var orderDetails = GetOrderDetailListByListOrder(order);
        
        foreach(var orderDetail in orderDetails) {
            total += orderDetail.Quantity * (double)orderDetail.UnitPrice - (orderDetail.Quantity * (double)orderDetail.UnitPrice * (orderDetail.Discount / 100));
        }

        return total;
    }

    public TblOrderDetail? GetOrderDetailById(int orderID, int productID) {
        TblOrderDetail? orderDetail;

        try {
            using SalesManagementContext context = new SalesManagementContext();
            orderDetail = context.TblOrderDetails.SingleOrDefault(oDetail => oDetail.OrderId == orderID && oDetail.ProductId == productID);
        }
        catch(Exception ex) {
            throw new Exception(ex.Message);
        }

        return orderDetail;
    }

    public void AddNew(TblOrderDetail orderDetail) {
        try {
            TblOrderDetail? oDetail = GetOrderDetailById(orderDetail.OrderId, orderDetail.ProductId);
            if(oDetail is null) {
                using SalesManagementContext context = new SalesManagementContext();
                context.TblOrderDetails.Add(oDetail);
                context.SaveChanges();
            }
            else {
                throw new Exception("Error: The Order Detail is already exists.");
            }
        }
        catch(Exception ex) {
            throw new Exception(ex.Message);
        }
    }

    public void Update(TblOrderDetail orderDetail) {
        try {
            TblOrderDetail? oDetail = GetOrderDetailById(orderDetail.OrderId, orderDetail.ProductId);
            if(oDetail is not null) {
                using SalesManagementContext context = new SalesManagementContext();
                context.TblOrderDetails.Update(orderDetail);
                context.SaveChanges();
            }
            else {
                throw new Exception("Error: This Order Detail doesn't exists.");
            }
        }
        catch(Exception ex) {
            throw new Exception(ex.Message);
        }
    }

    public void Remove(int orderID, int productID) {
        try {
            TblOrderDetail orderDetail = GetOrderDetailById(orderID, productID);
            if(orderDetail is not null) {
                using SalesManagementContext context = new SalesManagementContext();
                context.TblOrderDetails.Remove(orderDetail);
                context.SaveChanges();
            }
            else {
                throw new Exception("Error: This Order Detail doesn't exists");
            }
        }
        catch(Exception ex) {
            throw new Exception(ex.Message);
        }
    }
}
