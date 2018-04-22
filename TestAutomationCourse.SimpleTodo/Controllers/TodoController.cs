using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAutomationCourse.SimpleTodo.Web.Controllers.Model;
using TestAutomationCourse.SimpleTodo.Web.Data;

namespace TestAutomationCourse.SimpleTodo.Web.Controllers
{
    public class TodoController : Controller
    {
        private readonly DataContext context;

        public TodoController(DataContext context)
        {
            this.context = context;
        }

        public IActionResult Create()
        {

            return View();

        }
        [HttpPost]
        public IActionResult Create(CreateTodoItemModel model)
        {
            if (!ModelState.IsValid)
                return View();
            context.TodoItems.Add(new Domain.TodoItem
            {
                CreationDate = DateTime.Now,
                Title = model.Title,
                IsDone = false
            });
            context.SaveChanges();
            return RedirectToAction("List");
        }

        public IActionResult List()
        {
            var list = context.TodoItems.ToList();
            return View(list);
        }
    }
}
