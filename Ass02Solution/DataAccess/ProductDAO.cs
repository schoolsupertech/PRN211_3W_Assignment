using BusinessObject.Models;

namespace DataAccess; 
public class ProductDAO {
    private static ProductDAO instance;
    private static readonly object instanceLock = new object();
    private ProductDAO() { }
    public static ProductDAO Instance {
        get {
            lock(instanceLock) {
                if(instance == null) {
                    instance = new ProductDAO();
                }
                return instance;
            }
        }
    }

    public IEnumerable<TblProduct> GetProductList() {
        IEnumerable<TblProduct> products;

        try {
            using SalesManagementContext context = new SalesManagementContext();
            products = context.TblProducts.ToList();
        }
        catch(Exception ex) {
            throw new Exception(ex.Message);
        }

        return products;
    }

    public TblProduct? GetProductById(int id) {
        TblProduct? product;

        try {
            using SalesManagementContext context = new SalesManagementContext();
            product = context.TblProducts.SingleOrDefault(p => p.ProductId == id);
        }
        catch(Exception ex) {
            throw new Exception(ex.Message);
        }

        return product;
    }

    public List<TblProduct> Filter(string name, string unitPrice, string id) {
        var listProduct = GetProductList();
        List<TblProduct> products = new List<TblProduct>();

        try {
            if(!String.IsNullOrWhiteSpace(name)) {
                foreach(var product in listProduct) {
                    if(product.ProductName.Contains(name)) {
                        products.Add(product);
                    }
                }
            }
            if(!String.IsNullOrWhiteSpace(unitPrice)) {
                foreach(var p in listProduct) {
                    if(p.UnitPrice == decimal.Parse(unitPrice)) {
                        products.Add(p);
                    }
                }
            }
            if (!String.IsNullOrWhiteSpace(id)) {
                products.Add(GetProductById(int.Parse(id)));
            }
        }
        catch(Exception ex) {
            throw new Exception(ex.Message);
        }

        return products;
    }

    public List<TblProduct> GetProductByName(string name) {
        var listProduct = GetProductList();
        List<TblProduct> products = new List<TblProduct>();
        
        try {
            foreach(var p in listProduct) {
                if(p.ProductName.Contains(name)) {
                    products.Add(p);
                }
            }
        }
        catch(Exception ex) {
            throw new Exception(ex.Message);
        }

        return products;
    }

    public List<TblProduct> GetProductByUnitPrice(decimal param) {
        var listProduct = GetProductList();
        List<TblProduct> products = new List<TblProduct>();

        try {
            foreach(var p in listProduct) {
                if(p.UnitPrice == param) {
                    products.Add(p);
                }
            }
        }
        catch(Exception ex) {
            throw new Exception(ex.Message);
        }

        return products;
    }

    public void AddNew(TblProduct product) {
        try {
            TblProduct? tblProduct = GetProductById(product.ProductId);
            if(tblProduct is null) {
                using SalesManagementContext context = new SalesManagementContext();
                context.TblProducts.Add(tblProduct);
                context.SaveChanges();
            }
            else {
                throw new Exception("Error: This Product Id is already exists.");
            }
        }
        catch(Exception ex) {
            throw new Exception(ex.Message);
        }
    }

    public void Update(TblProduct p) {
        try {
            TblProduct? product = GetProductById(p.ProductId);
            if(product is not null) {
                using SalesManagementContext context = new SalesManagementContext();
                context.TblProducts.Update(p);
                context.SaveChanges();
            }
            else {
                throw new Exception("Error: The Product doesn't exists.");
            }
        }
        catch(Exception ex) {
            throw new Exception(ex.Message);
        }
    }

    public void Remove(int id) {
        try {
            TblProduct? product = GetProductById(id);
            if(product is not null) {
                using SalesManagementContext context = new SalesManagementContext();
                context.TblProducts.Remove(product);
                context.SaveChanges();
            }
            else {
                throw new Exception("Error: The Product doesn't exists.");
            }
        }
        catch(Exception ex) {
            throw new Exception(ex.Message);
        }
    }
}
