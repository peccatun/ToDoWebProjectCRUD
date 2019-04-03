using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class TaskController:Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            using (var db = new ToDoDbContext())
            {
                var allTasks = db.Tasks.ToList();
                return View(allTasks);
            }
        }
        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }
        [HttpPost]
        public IActionResult Create(string title,string comments)
        {
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(comments))
            {
                return RedirectToAction("Index");
            }
            Task task = new Task
            {
                Title = title,
                Comments = comments
            };
            using (var db = new ToDoDbContext())
            {
                db.Tasks.Add(task);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            using (var db = new ToDoDbContext())
            {
                var idToEdit = db.Tasks.FirstOrDefault(t => t.Id == id);
                if (idToEdit == null)
                {
                    return RedirectToAction("Index");
                }
                return this.View(idToEdit);
            }
        }
        [HttpPost]
        public IActionResult Edit(Task task)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            using (var db = new ToDoDbContext())
            {
                var taskToEdit = db.Tasks.FirstOrDefault(t => t.Id == task.Id);
                taskToEdit.Title = task.Title;
                taskToEdit.Comments = task.Comments;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Details (int id)
        {
            using (var db = new ToDoDbContext())
            {
                var idToRemove = db.Tasks.FirstOrDefault(t => t.Id == id);
                if (idToRemove == null)
                {
                    return RedirectToAction("Index");
                }
                return View(idToRemove);
            }
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            using (var db = new ToDoDbContext())
            {
                var taskToRemove = db.Tasks.FirstOrDefault(t => t.Id == id);
                if (taskToRemove == null)
                {
                    return RedirectToAction("Index");
                }
                db.Tasks.Remove(taskToRemove);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
