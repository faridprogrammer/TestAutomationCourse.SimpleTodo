using Microsoft.AspNetCore.Mvc;
using Phase2.SimpleTodo.Web.Services;
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
            var notificationService = new NotificationService();
            var service = new TodoService(context, notificationService);
            service.AddTodo(model);
            return RedirectToAction("List");
        }

        public IActionResult List()
        {
            var notificationService = new NotificationService();

            var service = new TodoService(context, notificationService);
            return View(service.GetAll());
        }
    }
}
