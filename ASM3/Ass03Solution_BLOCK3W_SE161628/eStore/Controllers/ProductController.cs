using BusinessObject.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;

namespace eStore.Controllers
{
    public class ProductController : Controller
    {
        IProductRepository Product = null;
        public ProductController() => Product = new ProductRepository();

        // GET: ProductController
        public ActionResult Index()
        {
            var Products = Product.GetProduct();
            
            return View(Products);
        }
        public ActionResult Search(string pro)
        {
            var Products = Product.SearchProductNames(pro);
            ICollection collection = Products as ICollection;
            var products = collection.Count;
            if (products != 0)
            {
               
                return View(Products);

            }
           
            
            if (pro != null)
            {
                decimal a = decimal.Parse(pro);
                var unit = Product.SearchUnitPrice(a);
                ICollection collections = unit as ICollection;
                var price = collections.Count;
                if (price != 0)
                {

                    return View(unit);

                }


            }
          

            return RedirectToAction("Index");
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var mem = Product.GetListByID(id.Value);
            if (mem == null)
            {
                return NotFound();
            }

            return View(mem);
        }

        // GET: ProductController/Create
        public ActionResult Create() => View();
       

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product pro)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Product.SaveProduct(pro);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(pro);
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var mem = Product.GetListByID(id.Value);
            if (mem == null)
            {
                return NotFound();
            }
            return View(mem);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Product mem)
        {
            try
            {
                if (id != mem.ProductId)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    Product.UpdateProduct(mem);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var mem = Product.GetListByID(id.Value);
            if (mem == null)
            {
                return NotFound();
            }
            return View(mem);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Product.DeleteProduct(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}
