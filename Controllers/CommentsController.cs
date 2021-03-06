﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task_Management_Platform.Models;

namespace Task_Management_Platform.Controllers
{
    public class CommentsController : Controller
    {
        private Models.AppData db = new Models.AppData();

        //POST: adaugare cometariu-ul nou in baza de date
        [HttpPost]
        public ActionResult New(Comment newComment)
        {
            newComment.DataAdaug = DateTime.Now;

            try
            {
                if (ModelState.IsValid)
                {
                    db.Comments.Add(newComment);
                    db.SaveChanges();
                    TempData["message"] = "Taskul a fost adaugat cu success!";

                    return Redirect("/Tasks/Show/" + newComment.TaskId);
                }

                TempData["message"] = "Nu s-a putut adauga comntariul!";
                return Redirect("/Tasks/Show/" + newComment.TaskId);
            }
            catch (Exception e)
            {
                ViewBag.Message = "Nu s-a putut adauga comentariul!";
                ViewBag.Message = e.Message;
                return Redirect("/Tasks/Show/" + newComment.TaskId);
            }
        }

        //EDIT
        //GET: afisare formular de editare comentariu
        public ActionResult Edit(int id)
        {
            Comment comment = db.Comments.Find(id);

            return View(comment);
        }

        //PUT: modificare comentariu
        [HttpPut]
        public ActionResult Edit(int id, Comment editedComment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Comment comment = db.Comments.Find(id);

                    if (TryUpdateModel(comment))
                    {
                        comment = editedComment;
                        db.SaveChanges();
                        TempData["message"] = "Task-ul a fost modificat cu succes!";

                        return Redirect("/Tasks/Show/" + comment.TaskId);
                    }

                    ViewBag.Message = "Task-ul a fost modificat cu succes!";
                    return View(editedComment);
                }

                return View(editedComment);
            }
            catch (Exception e)
            {
                ViewBag.Message = "Task-ul a fost modificat cu succes!";
                return View(editedComment);
            }
        }

        //DELETE
        //DELETE: stergerea unui comentariu
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Comment comment = db.Comments.Find(id);

            try
            {
                db.Comments.Remove(comment);
                db.SaveChanges();
                TempData["message"] = "Task-ul a fost sters cu success!";

                return Redirect("/Tasks/Show/" + comment.TaskId);
            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = e.Message;
                TempData["message"] = "A aparut o eroare la stergerea mesajului!";
                return Redirect("/Tasks/Show/" + comment.Task.TaskId);
            }
        }
    }
}