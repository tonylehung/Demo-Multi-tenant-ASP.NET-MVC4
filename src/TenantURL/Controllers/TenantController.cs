using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TenantURL.Models;
using TenantURL.Models.Entity;
using TenantURL.Filters;

namespace TenantURL.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class TenantController : Controller
    {
        private DefaultConnection db = new DefaultConnection();

        public ActionResult Index()
        {
            return View(db.TenantControl.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TenantControl tenantControl = db.TenantControl.Find(id);
            if (tenantControl == null)
            {
                return HttpNotFound();
            }
            return View(tenantControl);
        }
        
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TenantUser,Theme,TenantSchema,TenantConnection")] TenantControl tenantControl)
        {
            if (ModelState.IsValid)
            {
                db.TenantControl.Add(tenantControl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tenantControl);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TenantControl tenantControl = db.TenantControl.Find(id);
            if (tenantControl == null)
            {
                return HttpNotFound();
            }
            return View(tenantControl);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TenantUser,Theme,TenantSchema,TenantConnection")] TenantControl tenantControl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tenantControl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tenantControl);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TenantControl tenantControl = db.TenantControl.Find(id);
            if (tenantControl == null)
            {
                return HttpNotFound();
            }
            return View(tenantControl);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TenantControl tenantControl = db.TenantControl.Find(id);
            db.TenantControl.Remove(tenantControl);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
