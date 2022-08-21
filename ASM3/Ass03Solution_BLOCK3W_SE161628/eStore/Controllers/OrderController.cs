using BusinessObject.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace eStore.Controllers
{
    public class OrderController : Controller
    {
        // GET: OrderController

        IOrderRepository order = null;
        public OrderController() => order = new OderRepository();
        public ActionResult Index()
        {
            var orders = order.GetOrderList();

            return View(orders);
        }

        //GET: OrderController/Details/5
        public ActionResult Details(int? id)
        {
            var li = new OrderList();
            if (id == null)
            {
                return NotFound();
            }
            var mem = order.SearchOrderdetails(id.Value);
            foreach (var i in mem)
            {
                li.MemberId = i.MemberId;
                li.OrderId = i.OrderId;
                li.ProductId = i.ProductId;
                li.UnitPrice = i.UnitPrice;
                li.Quantity = i.Quantity;
                li.Discount = i.Discount;
                li.OrderDate = i.OrderDate;
                li.RequiredDate = i.RequiredDate;
                li.ShippedDate = i.ShippedDate;
                li.Freight = i.Freight;

            }
            if (mem == null)
            {
                return NotFound();
            }

            return View(li);
        }

        // GET: OrderController/Create
        public ActionResult Create() => View();
      

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order pro, OrderDetail b)
        {
            try
            {
                if (ModelState.IsValid)
                {
                  order.SaveOrderList(b,pro);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(pro);
            }
        }

      //  GET: OrderController/Edit/5
        public ActionResult Edit(int? id)
        {
            var li = new OrderList();
            if (id == null)
            {
                return NotFound();
            }
            var mem = order.SearchOrderdetails(id.Value);
            foreach (var i in mem)
            {
                li.MemberId = i.MemberId;
                li.OrderId = i.OrderId;
                li.ProductId = i.ProductId;
                li.UnitPrice = i.UnitPrice;
                li.Quantity = i.Quantity;
                li.Discount = i.Discount;
                li.OrderDate = i.OrderDate;
                li.RequiredDate = i.RequiredDate;
                li.ShippedDate = i.ShippedDate;
                li.Freight = i.Freight;

            }
            if (mem == null)
            {
                return NotFound();
            }
            return View(li);
        }

        // POST: OrderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Order mem, OrderDetail a)
        {
            try
            {
                if (id != mem.OrderId)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    order.UpdateOrderList(a,mem);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: OrderController/Delete/5
        public ActionResult Delete(int? id)
        {
            var li = new OrderList();
            if (id == null)
            {
                return NotFound();
            }
            var mem = order.SearchOrderdetails(id.Value);
           foreach (var i in mem) 
                {
               li.MemberId = i.MemberId;
                li.OrderId=i.OrderId;
                li.ProductId = i.ProductId;
                li.UnitPrice = i.UnitPrice;
                li.Quantity = i.Quantity;
                li.Discount = i.Discount;
                li.OrderDate = i.OrderDate;
                li.RequiredDate = i.RequiredDate;
                li.ShippedDate = i.ShippedDate;
                li.Freight = i.Freight;

            }
            if (mem == null)
            {
                return NotFound();
            }
            return View(li);
        }

        // POST: OrderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                order.DeleteOrderList(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}
