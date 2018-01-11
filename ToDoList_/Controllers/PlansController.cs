using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ToDoList_.Models;
using ToDoList_.Repository;

namespace ToDoList_.Controllers
{
    public class PlansController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ToDoListRepository repository = new ToDoListRepository();
        
        // GET: Plans
        public ActionResult Index()
        {
            string currentUserId = User.Identity.GetUserId();     
            return View(repository.GetAllPlans(currentUserId));
        }

        [HttpPost]
        public ActionResult AjaxEdit(int? id, bool value)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plan plan = db.Plans.Find(id);
            if (plan == null)
            {
                return HttpNotFound();
            }
            plan.Done = value;
            db.Entry(plan).State = EntityState.Modified;
            db.SaveChanges();
            string currentUserId = User.Identity.GetUserId();

            return View("Index", repository.GetAllPlans(currentUserId));
        }


        [HttpPost]
        public ActionResult EditDescription(int id,string text)
        {
            Plan plan = db.Plans.Find(id);
            if (text != String.Empty)
            {
                plan.Description = text;
                db.Entry(plan).State = EntityState.Modified;
                db.SaveChanges();
            }
            string currentUserId = User.Identity.GetUserId();
      
            return View("Index", repository.GetAllPlans(currentUserId));
        }

        // POST: Plans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PlanId,Description,Done")] Plan plan)
        {
            if (ModelState.IsValid)
            {
                string currentUserId = User.Identity.GetUserId();
                IdentityUser currentUser = db.Users.FirstOrDefault
                    (x => x.Id == currentUserId);
                plan.Done = false;
                plan.IdentityUser = currentUser;
                db.Plans.Add(plan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PlanModel = plan;
            return RedirectToAction("Index");
        }

        // POST: Plans/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Plan plan = db.Plans.Find(id);
            db.Plans.Remove(plan);
            db.SaveChanges();
            string currentUserId = User.Identity.GetUserId();

            return View("Index",repository.GetAllPlans(currentUserId));
        }

        // GET: All Done Plans

        public ActionResult AllDone()
        {           
            return View("Index", repository.GetAllDone(User.Identity.GetUserId()));
        }

        // GET: All Plans

        public ActionResult AllPlans()
        {     
            return View("Index", repository.GetAllPlans(User.Identity.GetUserId()));
        }

        // GET: All not Done Plans

        public ActionResult AllNotDone()
        {
            return View("Index", repository.GetAllNotDone(User.Identity.GetUserId()));
        }
       
        public ActionResult SaveAllPlansAfterLogin()
        {
            repository.SaveAllAfterLogin(User.Identity.GetUserId());
            return View("Index", repository.GetAllPlans(User.Identity.GetUserId()));
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
