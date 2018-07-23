using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookStore.DAL;
using BookStore.Models;

namespace BookStore.Controllers
{
    public class BooksController : Controller
    {
        private bookstore db = new bookstore();

        // GET: Books
        public ActionResult Index(string title = null, int price = 0, int categories = 0)
        {
            if (categories != 0)
            {
                var category_ = db.Category.Find(categories);
                ViewBag.categories = new SelectList(db.Category, "Id", "Name",category_);
            }
            else
                ViewBag.categories = new SelectList(db.Category, "Id", "Name");

            var books = db.Books.Include(b => b.Category).Include(b => b.Publishers);
            if (title != null)
            {
                if (title != "")
                {
                    books = books.Where(c => c.Title == title);
                }
            }
            if (price != 0)
            {
                books = books.Where(c => c.Price >= price);
            }
            if (categories != 0)
            {
                books = books.Where(c => c.CategoryId == categories);
            }

            return View(books.ToList());
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Books books = db.Books.Find(id);
            if (books == null)
            {
                return HttpNotFound();
            }
            return View(books);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Category, "Id", "Name");
            ViewBag.PublisherId = new SelectList(db.Publishers,"Id","Name");
            return View();
        }

        // POST: Books/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,AuthorList,Isbn,PublishingDate,PublisherId,CategoryId,PageCount,Price")] Books books)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(books);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Category, "Id", "Name", books.CategoryId);
            ViewBag.PublisherId = new SelectList(db.Publishers, "Id", "Name",books.PublisherId);
            return View(books);
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Books books = db.Books.Find(id);
            if (books == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Category, "Id", "Name", books.CategoryId);
            ViewBag.PublisherId = new SelectList(db.Publishers, "Id", "Name", books.PublisherId);
            return View(books);
        }

        // POST: Books/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,AuthorList,Isbn,PublishingDate,PublisherId,CategoryId,PageCount,Price")] Books books)
        {
            if (ModelState.IsValid)
            {
                db.Entry(books).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Category, "Id", "Name", books.CategoryId);
            return View(books);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Books books = db.Books.Find(id);
            if (books == null)
            {
                return HttpNotFound();
            }
            return View(books);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Books books = db.Books.Find(id);
            db.Books.Remove(books);
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
