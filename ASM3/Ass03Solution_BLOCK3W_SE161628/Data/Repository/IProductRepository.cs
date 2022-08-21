using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IProductRepository
    {
        List<Product> GetProduct();
        void SaveProduct(Product p);
        //List<Product> LoginMember(string p, string a);

        Product GetListByID(int p);

        void UpdateProduct(Product p);
        void DeleteProduct(int p);
        List<Product> SearchProductName(Product a);
        List<Product> SearchUnitPrice(decimal a);
        List<Product> SearchProductNames(string a);
    }
}
