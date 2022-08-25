using BusinessObject.Models;

namespace DataAccess.Repository; 
public interface IProductRepository {
    IEnumerable<TblProduct> GetProducts();
    TblProduct GetProductById(int id);
    void InsertProduct(TblProduct product);
    void UpdateProduct(TblProduct product);
    void DeleteProduct(int id);
    public List<TblProduct> Filter(string name, string unitPrice, string id);
    public List<TblProduct> GetProductByUnitPrice(decimal param);
    public List<TblProduct> GetProductByName(string param);
}
