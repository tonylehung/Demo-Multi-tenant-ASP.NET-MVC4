using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TenantURL.Filters;
using TenantURL.Models;
using TenantURL.Models.Entity;

namespace TenantURL.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            TenantControl tenant = new TenantControl
            {
                Id = -1,
                TenantUser = "anonymuos",
                Theme = "default",
                TenantSchema = "default"
            };

            if (Request.IsAuthenticated) {
                using (var db = new DefaultConnection()) {
                    string username = User.Identity.Name.Trim().ToLower();
                    var idata = db.TenantControl.FirstOrDefault(c => c.TenantUser.ToLower() == username);
                    if (idata != null)
                        tenant = idata;
                }
            }
            if (!tenant.TenantSchema.Equals("default"))
            {

            }
            return View(tenant);
        }
    }
}
