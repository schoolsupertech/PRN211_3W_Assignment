using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OrderDAO
    {
        public static List<OrderList> SearchOrderdetails(int p)
        {
            var list = new List<OrderList>();
            try
            {
                using (var db = new FStoreDBContext())
                {


                    var lists = (from s in db.OrderDetails
                                 join c in db.Orders on s.OrderId equals c.OrderId

                                 select new
                                 {
                                     s.OrderId,
                                     s.ProductId,
                                     s.UnitPrice,
                                     s.Quantity,
                                     s.Discount,
                                     c.MemberId,
                                     c.OrderDate,
                                     c.RequiredDate,
                                     c.ShippedDate,
                                     c.Freight,

                                 }).Where(x => x.OrderId == p);
                    foreach (var l in lists)
                    {
                        OrderList ord = new OrderList()
                        {
                            OrderId = l.OrderId,
                            MemberId = l.MemberId,
                            OrderDate = l.OrderDate,
                            RequiredDate = l.RequiredDate,
                            ShippedDate = l.ShippedDate,
                            Freight = l.Freight,
                            Discount = l.Discount,
                            Quantity = l.Quantity,
                            UnitPrice = l.UnitPrice,
                            ProductId = l.ProductId,

                        };


                        list.Add(ord);
                    }

                    db.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public static List<OrderList> SearchOrderdetailbyOrderID(int p)
        {
            var list = new List<OrderList>();
            try
            {
                using (var db = new FStoreDBContext())
                {


                    var lists = (from s in db.OrderDetails
                                 join c in db.Orders on s.OrderId equals c.OrderId

                                 select new
                                 {
                                     s.OrderId,
                                     s.ProductId,
                                     s.UnitPrice,
                                     s.Quantity,
                                     s.Discount,
                                     c.MemberId,
                                     c.OrderDate,
                                     c.RequiredDate,
                                     c.ShippedDate,
                                     c.Freight,

                                 }).Where(x => x.MemberId == p);
                    foreach (var l in lists)
                    {
                        OrderList ord = new OrderList()
                        {
                            OrderId = l.OrderId,
                            MemberId = l.MemberId,
                            OrderDate = l.OrderDate,
                            RequiredDate = l.RequiredDate,
                            ShippedDate = l.ShippedDate,
                            Freight = l.Freight,
                            Discount = l.Discount,
                            Quantity = l.Quantity,
                            UnitPrice = l.UnitPrice,
                            ProductId = l.ProductId,

                        };


                        list.Add(ord);
                    }

                    db.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }




        public static List<OrderList> GetOrderList()
        {
            var list = new List<OrderList>();
            try
            {
                using (var db = new FStoreDBContext())
                {


                    var lists = (from s in db.OrderDetails
                                 join c in db.Orders on s.OrderId equals c.OrderId

                                 select new
                                 {
                                     s.OrderId,
                                     s.ProductId,
                                     s.UnitPrice,
                                     s.Quantity,
                                     s.Discount,
                                     c.MemberId,
                                     c.OrderDate,
                                     c.RequiredDate,
                                     c.ShippedDate,
                                     c.Freight,

                                 });
                    foreach (var l in lists)
                    {
                        OrderList ord = new OrderList()
                        {
                            OrderId = l.OrderId,
                            MemberId = l.MemberId,
                            OrderDate = l.OrderDate,
                            RequiredDate = l.RequiredDate,
                            ShippedDate = l.ShippedDate,
                            Freight = l.Freight,
                            Discount = l.Discount,
                            Quantity = l.Quantity,
                            UnitPrice = l.UnitPrice,
                            ProductId = l.ProductId,

                        };


                        list.Add(ord);
                    }

                    db.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }



        public static void SaveOrderList(OrderDetail p, Order a)
        {

            try
            {
                Order pro = GetListByID(p.OrderId);
                if(pro == null)
                {
                    using var db = new FStoreDBContext();

                    db.OrderDetails.Add(p);
                    db.Orders.Add(a);
                    db.SaveChanges();

                }
                else
                {
                    throw new Exception(" the Order was Exist");
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
        public static Order GetListByID(int p)
        {
            var mem = new Order();
            try
            {
                using (var db = new FStoreDBContext())
                {
                    mem = db.Orders.SingleOrDefault(c => c.OrderId == p);
                    db.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return mem;
        }



        public static void UpdateOrder(OrderDetail p, Order a)
        {

            try
            {
                if (GetListByID(p.OrderId) != null)
                {
                    using (var db = new FStoreDBContext())
                    {

                        // db.Entry<Member>(p).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        db.OrderDetails.Update(p);
                        db.Orders.Update(a);
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
        public static void DeleteOrder(int id)
        {

            try
            {
                using (var db = new FStoreDBContext())
                {
                    var b = db.OrderDetails.SingleOrDefault(c => c.OrderId == id);
                    db.OrderDetails.Remove(b);
                    var o = db.Orders.FirstOrDefault(c => c.OrderId == id);
                    db.Orders.Remove(o);
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
