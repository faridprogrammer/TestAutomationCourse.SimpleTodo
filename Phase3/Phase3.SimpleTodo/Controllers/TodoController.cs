using Microsoft.AspNetCore.Mvc;
using Phase3.SimpleTodo.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAutomationCourse.SimpleTodo.Web.Data;
using TestAutomationCourse.SimpleTodo.Web.Dto;

namespace Phase3.SimpleTodo.Web.Controllers
{
    public class TodoController : Controller
    {
        private readonly DataContext context;
        private readonly ITodoService todoService;

        public TodoController(DataContext context, ITodoService todoService)
        {
            this.context = context;
            this.todoService = todoService;
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
            todoService.AddTodo(model);
            return RedirectToAction("List");
        }

        public IActionResult List()
        {
            return View(todoService.GetAll());
        }
    }
}
