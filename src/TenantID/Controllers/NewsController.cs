using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TenantID.Models.Entity;
using source.Models.Proxy;
using System.Data.SqlClient;
using TenantID.Models;

namespace TenantID.Controllers
{
    public class NewsController : Controller
    {

        private TenantControl getTenantControl()
        {
            
            TenantControl tenant = new TenantControl
            {
                Id = -1,
                TenantUser = "anonymuos",
                Theme = "default",
                TenantSchema = "default"
            };

            if (Request.IsAuthenticated)
            {
                using (var db = new DefaultConnection())
                {
                    string username = User.Identity.Name.Trim().ToLower();
                    tenant = db.TenantControl.FirstOrDefault(c => c.TenantUser.ToLower() == username);
                }
            }
            return tenant;
        }
        public ActionResult Index()
        {
            if (!Request.IsAuthenticated) return RedirectToAction("index", "home");

            var news =new List<News>();
            var tc = getTenantControl();
            if (tc==null)
                return RedirectToAction("index", "home");
            if (tc.Id!=-1)
                using (var connection = new SqlConnection(tc.TenantConnection)){
                    TenantConnection.ProvisionTenant(tc.TenantSchema, connection);
                    using (var ctx = TenantConnection.Create(tc.TenantSchema, connection)){
                        news = ctx.News.ToList();
                    }
                }
            return View(news);
        }

        public ActionResult Create()
        {
            if (!Request.IsAuthenticated) return RedirectToAction("index", "home");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Content,Created")] News news)
        {
            if (!Request.IsAuthenticated) return RedirectToAction("index", "home");
            if (ModelState.IsValid)
            {

                var tc = getTenantControl();
                if (tc.Id != -1)
                    using (var connection = new SqlConnection(tc.TenantConnection))
                    {
                        TenantConnection.ProvisionTenant(tc.TenantSchema, connection);
                        using (var ctx = TenantConnection.Create(tc.TenantSchema, connection))
                        {
                            ctx.News.Add(news);
                            ctx.SaveChanges();
                            return RedirectToAction("index");
                        }
                    }
            }

            return View(news);
        }

        public ActionResult Edit(int? id)
        {
            if (!Request.IsAuthenticated) return RedirectToAction("index", "home");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            News news = null;


            var tc = getTenantControl();
            if (tc.Id != -1)
                using (var connection = new SqlConnection(tc.TenantConnection))
                {
                    TenantConnection.ProvisionTenant(tc.TenantSchema, connection);
                    using (var ctx = TenantConnection.Create(tc.TenantSchema, connection))
                    {
                        news = ctx.News.Find(id);
                    }
                }
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Content,Created")] News news)
        {
            if (!Request.IsAuthenticated) return RedirectToAction("index", "home");
            if (ModelState.IsValid)
            {
                var tc = getTenantControl();
                if (tc.Id != -1)
                    using (var connection = new SqlConnection(tc.TenantConnection))
                    {
                        TenantConnection.ProvisionTenant(tc.TenantSchema, connection);
                        using (var db = TenantConnection.Create(tc.TenantSchema, connection))
                        {
                            db.Entry(news).State = EntityState.Modified;
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                    }
            }
            return View(news);
        }

        public ActionResult Delete(int? id)
        {
            if (!Request.IsAuthenticated) return RedirectToAction("index", "home");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = null;

            var tc = getTenantControl();
            if (tc.Id != -1)
                using (var connection = new SqlConnection(tc.TenantConnection))
                {
                    TenantConnection.ProvisionTenant(tc.TenantSchema, connection);
                    using (var db = TenantConnection.Create(tc.TenantSchema, connection))
                    {
                        news = db.News.Find(id);
                    }
                }

            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!Request.IsAuthenticated) return RedirectToAction("index", "home");
            var tc = getTenantControl();
            if (tc.Id != -1)
                using (var connection = new SqlConnection(tc.TenantConnection))
                {
                    TenantConnection.ProvisionTenant(tc.TenantSchema, connection);
                    using (var db = TenantConnection.Create(tc.TenantSchema, connection))
                    {
                        News news  = db.News.Find(id);
                        db.News.Remove(news);
                        db.SaveChanges();
                    }
                }

            return RedirectToAction("Index");
        }
        
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
