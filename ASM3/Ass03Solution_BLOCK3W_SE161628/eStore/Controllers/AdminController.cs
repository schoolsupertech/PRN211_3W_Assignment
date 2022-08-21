using BusinessObject.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace eStore.Controllers
{
    public class AdminController : Controller
    {
        IMemberRepository member = null;
        public AdminController() => member = new MemberRepository();
        // GET: AdminController
        public ActionResult Index()
        {
            var members = member.GetMember();
          
            return View(members);
        }

        // GET: AdminController/Details/5
        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var mem = member.GetListByIDs(id.Value);
            if(mem == null)
            {
                return NotFound();
            }

            return View(mem);
        }

        // GET: AdminController/Create
        public ActionResult Create() => View();
      

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Member mem)
        {
            
            try
            {
                var po = member.GetMemberfromEmail(mem);
               if ( po != null)
                {
                    ViewBag.error = "Email exist !! ";
                    return View("Edit");
     
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        member.SaveMember(mem);
                    }
                    return RedirectToAction(nameof(Index));
                }
               
            }
            catch(Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(mem);
            }
        }

        // GET: AdminController/Edit/5
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var mem = member.GetListByIDs(id.Value);
            if(mem == null)
            {
               
            }
            return View(mem);
        }

        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Member mem)
        {
            try
            {
              if(id != mem.MemberId)
                {
                    return NotFound();
                }

                var po = member.GetMemberfromEmail(mem);
                if (po != null)
                {
                    ViewBag.error = "Email exist !! ";
                    return View("Edit");

                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        member.UpdateMember(mem);
                    }
                    return RedirectToAction(nameof(Index));
                }
             
                    
               
            }
            catch(Exception ex)
            {

                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: AdminController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var mem = member.GetListByIDs(id.Value);
            if (mem == null)
            {
                return NotFound();
            }
            return View(mem);

        }

        // POST: AdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
               member.DeleteMember(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}
