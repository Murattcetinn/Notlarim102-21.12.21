using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Notlarim102.BusinessLayer;
using Notlarim102.Entity;
using Notlarim102.WebApp.Models;

namespace Notlarim102.WebApp.Controllers
{
    public class NoteController : Controller
    {
        NoteManager nm = new NoteManager();
        CategoryManager cm = new CategoryManager();
        public ActionResult Index()
        {
            //var notes1 = db.Notes.Include(n => n.Category);

            //var user = HttpContext.Session["login"] as NotlarimUser;
            var notes = nm.QList().Include("Category").Include("Owner").Where(s => s.Owner.Id == CurrentSession.User.Id).OrderByDescending(s=>s.ModifiedOn);

            return View(notes.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = nm.Find(s=>s.Id==id);
            if (note == null)
            {
                return HttpNotFound();
            }
            return View(note);
        }

        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(cm.List(), "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Note note)

        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {
                //db.Notes.Add(note);
                //db.SaveChanges();
                note.Owner = CurrentSession.User;
                nm.Insert(note);
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(cm.List(), "Id", "Title", note.CategoryId);
            return View(note);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = nm.Find(s => s.Id == id);
            if (note == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(cm.List(), "Id", "Title", note.CategoryId);
            return View(note);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Note note)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {
                //db.Entry(note).State = EntityState.Modified;
                //db.SaveChanges();
                Note dbNote = nm.Find(s => s.Id == note.Id);
                dbNote.IsDraft = note.IsDraft;
                dbNote.CategoryId = note.CategoryId;
                dbNote.Text = note.Text;
                dbNote.Title = note.Title;
                nm.Update(dbNote);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(cm.List(), "Id", "Title", note.CategoryId);
            return View(note);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = nm.Find(s => s.Id == id);
            if (note == null)
            {
                return HttpNotFound();
            }
            return View(note);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Note note = nm.Find(s => s.Id == id);
            //db.Notes.Remove(note);
            //db.SaveChanges();
            nm.Delete(note);
            return RedirectToAction("Index");
        }

      
    }
}
