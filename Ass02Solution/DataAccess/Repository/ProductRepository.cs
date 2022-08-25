using BusinessObject.Models;

namespace DataAccess.Repository;
public class ProductRepository : IProductRepository {
    public void DeleteProduct(int id) => ProductDAO.Instance.Remove(id);

    public List<TblProduct> Filter(string name, string unitPrice, string id) => ProductDAO.Instance.Filter(name, unitPrice, id);

    public TblProduct? GetProductById(int id) => ProductDAO.Instance.GetProductById(id);

    public List<TblProduct> GetProductByName(string param) => ProductDAO.Instance.GetProductByName(param);

    public List<TblProduct> GetProductByUnitPrice(decimal param) => ProductDAO.Instance.GetProductByUnitPrice(param);

    public IEnumerable<TblProduct> GetProducts() => ProductDAO.Instance.GetProductList();

    public void InsertProduct(TblProduct product) => ProductDAO.Instance.AddNew(product);

    public void UpdateProduct(TblProduct product) => ProductDAO.Instance.Update(product);
}
