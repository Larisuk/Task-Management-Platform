﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task_Management_Platform.Models;

namespace Task_Management_Platform.Controllers
{
    public class TasksController : Controller
    {
        private Models.AppData db = new Models.AppData();

        //INDEX
        // GET: Tasks (afisarea tuturor taskurilor)
        public ActionResult Index()
        {
            if (TempData.ContainsKey("message"))
                ViewBag.Message = TempData["message"];

            var tasks = db.Tasks;
            ViewBag.Tasks = tasks;

            return View();
        }

        //SHOW
        //GET: afisarea unui singur task
        public ActionResult Show(int id)
        {
            if (TempData.ContainsKey("message"))
                ViewBag.Message = TempData["message"];

            Task task = db.Tasks.Find(id);
            
            return View(task);
        }

        //NEW
        //GET: afisare formular adaugare task
        public ActionResult New()
        { 
            return View();
        }

        //POST: adaugare task-ul nou in baza de date
        [HttpPost]
        public ActionResult New(Task newTask)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Tasks.Add(newTask);
                    db.SaveChanges();
                    TempData["message"] = "Taskul a fost adaugat cu success!";

                    return RedirectToAction("Index");
                }

                return View(newTask);
            }
            catch(Exception e)
            {
                ViewBag.Message = "Nu s-a putut adauga task-ul!";
                return View(newTask);
            }
        }

        //EDIT
        //GET: afisare formular de editare task
        public ActionResult Edit(int id)
        {
            Task task = db.Tasks.Find(id);

            return View(task);
        }

        //PUT: modificare task
        [HttpPut]
        public ActionResult Edit(int id, Task editedTask)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Task task = db.Tasks.Find(id);

                    if (TryUpdateModel(task))
                    {
                        task = editedTask;
                        db.SaveChanges();
                        TempData["message"] = "Task-ul a fost modificat cu succes!";

                        return Redirect("/Tasks/Show/" + id);
                    }

                    ViewBag.Message = "Nu s-a putut edita task-ul!";
                    return View(editedTask);
                }

                return View(editedTask);
            }
            catch(Exception e)
            {
                ViewBag.Message = "Nu s-a putut edita task-ul!";
                return View(editedTask);
            }
        }

        //DELETE
        //DELETE: stergerea unui task
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Task task = db.Tasks.Find(id);

            try
            {
                db.Tasks.Remove(task);
                db.SaveChanges();
                TempData["message"] = "Task-ul a fost sters cu success!";

                return RedirectToAction("Index");
            }
            catch (Exception e)
            { 
                TempData["message"] = "Nu s-a putut sterge task-ul!";
                return Redirect("/Tasks/Show/" + task.TaskId);
            }
        }
    }
}