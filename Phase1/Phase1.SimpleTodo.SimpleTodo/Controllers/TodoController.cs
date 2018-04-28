using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAutomationCourse.SimpleTodo.Web.Data;
using TestAutomationCourse.SimpleTodo.Web.Dto;
using TestAutomationCourse.SimpleTodo.Web.Services;

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
        public IActionResult Create(TodoDto model)
        {
            if (!ModelState.IsValid)
                return View();
            var service = new TodoService(context);
            service.AddTodo(model);
            return RedirectToAction("List");
        }

        public IActionResult List()
        {
            var service = new TodoService(context);
            return View(service.GetAll());
        }
    }
}
