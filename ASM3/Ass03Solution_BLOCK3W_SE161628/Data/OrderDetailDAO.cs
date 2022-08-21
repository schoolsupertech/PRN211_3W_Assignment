using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OrderDetailDAO
    {
        public static List<OrderDetail> GetMember()
        {
            var list = new List<OrderDetail>();
            try
            {
                using (var db = new FStoreDBContext())
                {
                    list = db.OrderDetails.ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return list;
        }



        public static void SaveMember(OrderDetail p)
        {

            try
            {
                using (var db = new FStoreDBContext())
                {
                    db.OrderDetails.Add(p);
                    db.SaveChanges();
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
        public static OrderDetail GetListByID(int p)
        {
            var mem = new OrderDetail();
            try
            {
                using (var db = new FStoreDBContext())
                {
                    mem = db.OrderDetails.SingleOrDefault(c => c.OrderId == p);
                    db.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return mem;
        }



        public static void UpdateMember(OrderDetail p)
        {

            try
            {
                if (GetListByID(p.OrderId) == null)
                {
                    using (var db = new FStoreDBContext())
                    {

                        // db.Entry<Member>(p).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        db.OrderDetails.Update(p);
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
        public static void DeleteOrderDetail(int p)
        {

            try
            {
                using (var db = new FStoreDBContext())
                {
                    var b = db.OrderDetails.SingleOrDefault(c => c.OrderId == p);
                    db.OrderDetails.Remove(b);
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
