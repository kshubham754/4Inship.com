﻿using _4InShip.com.Areas.Admin.Services;
using _4InShip.com.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static _4InShip.com.FilterConfig;
namespace _4InShip.com.Areas.Admin.Controllers
{
    [CustomAuthorization(Url = "~/Admin/Login")]
    [Authorize(Roles = "Admin")]
    [IsAuthenticateAdminFilter]
    public class AdminUsersController : Controller
    {
        // GET: Admin/AdminUsers
        AdminUsersService Context = new AdminUsersService();
        public ActionResult Index(int? Id)
        {
            if (Id != null)
            {

                return View(Context.GettbltblAdminUserById(Id ?? 0).SingleOrDefault());
            }
            return View();
        }
        [ValidateAntiForgeryToken]
        public ActionResult InsertTblAdminUsers(tblAdminUser tbladminuser)
        {
            try
            {
                if (tbladminuser != null)
                {
                    ViewBag.Message = Context.InsertUpdateAdminUsers(tbladminuser);
                    
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;

            }
            tblAdminUser roll = new tblAdminUser();
            ModelState.Clear();
            return View("Index");
        }
        public ActionResult UpdateTblAdminUsers(tblAdminUser tbladminuser)
        {

            try
            {
                if (tbladminuser != null)
                {
                    Context.InsertUpdateAdminUsers(tbladminuser);
                }

            }
            catch (Exception ex)
            {
                var err = ex.Message;

            }
            tblAdminUser roll = new tblAdminUser();
            ModelState.Clear();
            return View("Index", roll);
        }
        public ActionResult AdminUsers()
        {
            tblAdminUser roll = new tblAdminUser();

            List<tblAdminUser> lst = Context.GettbltblAdminUser().ToList();

            return View("GetAdminUsersDetail", lst);
        }
        public JsonResult DeleteAdminUsersDetailById(int Id)
        {
            if (Id != 0)
            {
                Context.DeletetblAdminUser(Id);
            }
            return Json(Id.ToString(), JsonRequestBehavior.AllowGet);

        }

        public JsonResult ChangeStatusTblUserById(int Id)
        {
            if (Id!=0)
            {
               ViewBag.Message= Context.ChangeStatusTblUserById(Id);
            }
            return Json(Id.ToString(), JsonRequestBehavior.AllowGet);
        }
    }
}