using BusinessObject.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace eStore.Controllers
{
    public class MemberController : Controller
    {
        IMemberRepository members = null;
        public MemberController() => members = new MemberRepository();
 
        // GET: AdminController
        public ActionResult Index(Member mem)
        {
            var member =members.GetMemberfromEmails(mem.Email);

            return View(member);

        }
        public ActionResult Orders(int? id)
        {
            var mem = new Member();
            if (id == null)
            {
                return NotFound();
            }
            var list = members.GetListByID(id.Value);

            foreach (var member in list)
            {
                mem.Email = member.Email;
                mem.Password = member.Password;
                mem.City = member.City;
                mem.MemberId = member.MemberId;
                mem.CompanyName = member.CompanyName;
                mem.Country = member.Country;
            }
            return RedirectToAction("Index", "OrderMember", mem);
            

        }



        // member12@fstore.com
        // GET: AdminController/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var mem = members.GetListByIDs(id.Value);
            if (mem == null)
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
                if (ModelState.IsValid)
                {
                    members.SaveMember(mem);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(mem);
            }
        }

        // GET: AdminController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var mem = members.GetListByIDs(id.Value);
            if (mem == null)
            {
                return NotFound();
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
                if (id != mem.MemberId)
                {
                    return NotFound();
                }
                 var po = members.GetMemberfromEmail(mem);
                if (po != null)
                {
                    ViewBag.error = "Email exist !! ";
                    return View("Edit");

                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        members.UpdateMember(mem);
                    }
                    return RedirectToAction(nameof(Index));
                }
                }
            catch (Exception ex)
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
            var mem = members.GetListByIDs(id.Value);
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
                members.DeleteMember(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}
