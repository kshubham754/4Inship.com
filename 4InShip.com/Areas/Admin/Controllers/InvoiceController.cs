﻿using _4InShip.com.Areas.Admin.Models;
using _4InShip.com.Areas.Admin.Services;
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
    public class InvoiceController : Controller
    {
        // GET: Admin/Invoice
        InvoiceService Context = new InvoiceService();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Invoices()
        {
            List<ViewModelInvoice> lst = Context.GetTblInvoiceDetail();
            return View("GetInvoiceDetail",lst);

        }
    }
}