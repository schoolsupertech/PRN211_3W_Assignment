using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ProductRepository : IProductRepository
    {
        public void DeleteProduct(int p)
        {
            ProductDAO.DeleteProduct(p);
        }

        public Product GetListByID(int p) => ProductDAO.GetListByID(p);
        

        public List<Product> GetProduct() => ProductDAO.GetProduct();
     

        public void SaveProduct(Product p)
        {
            ProductDAO.SaveProduct(p);
        }

        public List<Product> SearchProductName(Product a) => ProductDAO.SearchProductName(a);

       

        public List<Product> SearchProductNames(string a) => ProductDAO.SearchProductNames(a);


        public List<Product> SearchUnitPrice(decimal a) => ProductDAO.SearchUnitPrice(a);
        

        public void UpdateProduct(Product p)
        {
            ProductDAO.UpdateProduct(p);
        }
    }
}
