﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using RaisingHeltons.Helpers;
using RaisingHeltons.Models;

namespace RaisingHeltons.Controllers
{
    public class BlogPostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BlogPosts
        public ActionResult Index()
        {
            return View(db.BlogPosts.ToList());
        }

        // GET: BlogPosts/Details/5
        public ActionResult Details(string Slug)
        {
            if (String.IsNullOrWhiteSpace(Slug))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.BlogPosts.FirstOrDefault(p => p.Slug == Slug);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }

        // GET: BlogPosts/Create
        public ActionResult Create()
        {
            ViewBag.CategoryIds = new SelectList(db.Categories.ToList(), "Id", "Name");
            return View();
        }

        // POST: BlogPosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,Body,Abstract,Published")] BlogPost blogPost, List<int> categoryIds)
        {
            if (ModelState.IsValid)
            {
                var slug = StringUtilities.URLFriendly(blogPost.Title);

                if (String.IsNullOrWhiteSpace(slug))
                {
                    ModelState.AddModelError("Title", "Invalid Title");
                    return View(blogPost);
                }
                if (db.BlogPosts.Any(p => p.Slug == slug))
                {
                    ModelState.AddModelError("Title", "The Title appears to have been used before and must be unique");
                    return View(blogPost);
                }

                blogPost.Slug = slug;
                blogPost.Created = DateTime.Now;

                //Slug code will eventually go here


                db.BlogPosts.Add(blogPost);
                db.SaveChanges();

                if (categoryIds != null)
                {
                    foreach (var categoryId in categoryIds)
                    {
                        db.CategoryBlogPosts.Add(new CategoryBlogPost
                        {
                            BlogPostId = blogPost.Id,
                            CategoryId = categoryId
                        });
                    }
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            return View(blogPost);
        }

        // GET: BlogPosts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.BlogPosts.Find(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }

        // POST: BlogPosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Slug,Body,Abstract,Created,Published")] BlogPost blogPost)
        {
            if (ModelState.IsValid)
            {
                var slug = StringUtilities.URLFriendly(blogPost.Title);

                if(slug != blogPost.Slug)
                {
                    if (String.IsNullOrWhiteSpace(slug))
                    {
                        ModelState.AddModelError("Title", "Invalid Title");
                        return View(blogPost);
                    }
                    
                    if (db.BlogPosts.Any(p => p.Slug == slug))
                    {
                        ModelState.AddModelError("Title", "The Title appears to have been used before and must be unique");
                        return View(blogPost);
                    }

                    blogPost.Slug = slug;
                }

                blogPost.Updated = DateTime.Now;

                db.Entry(blogPost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blogPost);
        }

        // GET: BlogPosts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.BlogPosts.Find(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }

        // POST: BlogPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogPost blogPost = db.BlogPosts.Find(id);
            db.BlogPosts.Remove(blogPost);
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
