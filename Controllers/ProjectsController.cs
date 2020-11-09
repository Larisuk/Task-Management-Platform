﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task_Management_Platform.Models;


namespace Task_Management_Platform.Controllers
{
    public class ProjectsController : Controller
    {
        private Models.AppData db = new Models.AppData();

        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(Project project)
        {
            try
            {
                db.Projects.Add(project);
                TempData["message"] = "Proiectul a fost adaugat cu succes.";
                db.SaveChanges();
                return Redirect("/Teams/Show/" + project.TeamId);
            }catch(Exception e)
            {
                ViewBag.Message = "Nu s-a putut adauga proiectul.";
                return View(project);
            }
        }

        public ActionResult Show(int id)
        {
            if (TempData.ContainsKey("message"))
                ViewBag.Message = TempData["message"];
            Project project = db.Projects.Find(id);
            return View(project);
        }

        public ActionResult Edit(int id)
        {
            Project proj = db.Projects.Find(id);
            return View(proj);
        }
        [HttpPut]
        public ActionResult Edit(int id,Project projectNew)
        {
            try
            {
                Project project = db.Projects.Find(id);
                if (TryUpdateModel(projectNew))
                {
                    project = projectNew;
                    db.Projects.Add(project);
                    db.SaveChanges();
                    return Redirect("/Teams/Show/" + project.TeamId);
                }

            }
            catch(Exception e)
            {
                ViewBag.message = "Nu s-a putut adauga proiectul.";
                return View(projectNew);
            }
            return View(projectNew);
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            try
            {
                Project prj = db.Projects.Find(id);
                if (TryUpdateModel(prj))
                {
                    var team_ID = prj.TeamId;
                    db.Projects.Remove(prj);
                    TempData["message"] = "Proiectul a fost sters u succes!";
                    return RedirectToAction("/Teams/Show/"+team_ID);
                }

            }
            catch (Exception e)
            {
                ViewBag.message = "Proiectul nu poate fi sters!";
                return View("/Projects/Show/" + id);
            }
            ViewBag.message = "Proiectul nu poate fi sters";
            return View("/Projects/Show/" + id);
        }
    }
}