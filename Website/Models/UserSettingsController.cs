﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Website.Models
{
    public class UserSettingsController : Controller
    {
        // GET: UserSettings
        public ActionResult Index()
        {
            return View();
        }
    }
}