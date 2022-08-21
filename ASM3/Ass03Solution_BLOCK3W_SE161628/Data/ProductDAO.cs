using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ProductDAO
    {
        public static List<Product> GetProduct()
        {
            var list = new List<Product>();
            try
            {
                using (var db = new FStoreDBContext())
                {
                    list = db.Products.ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return list;
        }
        public static List<Product> SearchProductName(Product a)
        {
            var list = new List<Product>();
            try
            {
                using (var db = new FStoreDBContext())
                {
                    list = db.Products.Where(x => x.ProductId ==a.ProductId).ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return list;
        }
        public static List<Product> SearchProductNames(string a)
        {
            var list = new List<Product>();
            try
            {
                using (var db = new FStoreDBContext())
                {
                   
                    list = db.Products.Where(x => x.ProductName.Contains(a)).ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return list;
        }
        public static List<Product> SearchUnitPrice(decimal a)
        {
            var list = new List<Product>();
            try
            {
                using (var db = new FStoreDBContext())
                {
                    list = db.Products.Where(x => x.UnitPrice == a).ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return list;
        }


        public static void SaveProduct(Product p)
        {

            try
            {
                Product pro = GetListByID(p.CategoryId);
                if(pro == null)
                {
                    using var db = new FStoreDBContext();

                        db.Products.Add(p);
                    db.SaveChanges();
                }
                else
                {
                    throw new Exception(" the Product was Exist");
                }
              
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        //public static List<Member> LoginMember(string p, string a)
        //{

        //    var mem = new List<Member>();
        //    try
        //    {
        //        using (var db = new FStoreDBContext())
        //        {
        //            mem = db.Members.Where(c => c.Email == p && c.Password == a).ToList();

        //            db.SaveChanges();

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //    return mem;
        //}
        public static Product GetListByID(int p)
        {
            var mem = new Product();
            try
            {
                using (var db = new FStoreDBContext())
                {
                    mem = db.Products.SingleOrDefault(c => c.ProductId == p);
                    db.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return mem;
        }



        public static void UpdateProduct(Product p)
        {

            try
            {
                if (GetListByID(p.ProductId) != null)
                {
                    using (var db = new FStoreDBContext())
                    {

                        // db.Entry<Member>(p).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        db.Products.Update(p);
                        db.SaveChanges();
                    }
                }
                else
                {
                    throw new Exception("the member is already update");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public static void DeleteProduct(int p)
        {

            try
            {
                using (var db = new FStoreDBContext())
                {
                    var b = db.Products.SingleOrDefault(c => c.ProductId == p);
                    db.Products.Remove(b);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
