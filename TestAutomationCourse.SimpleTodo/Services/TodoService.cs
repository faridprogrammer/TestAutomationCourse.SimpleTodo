using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAutomationCourse.SimpleTodo.Web.Data;
using TestAutomationCourse.SimpleTodo.Web.Dto;

namespace TestAutomationCourse.SimpleTodo.Web.Services
{
    public class TodoService
    {
        private readonly DataContext context;

        public TodoService(DataContext context)
        {
            this.context = context;
        }
        public void AddTodo(TodoDto input)
        {
            var exists = context.TodoItems.Any(ff => ff.Title.ToLower() == input.Title.ToLower());
            if (exists)
                throw new InvalidOperationException("Duplicate todo item");
            // refactor to mapper
            var todoEntity = new Domain.TodoItem
            {
                CreationDate = DateTime.Now,
                Title = input.Title,
                IsDone = false
            };

            // refactor to validator or guard
            var now = DateTime.Now;
            var todayCount = context.TodoItems.Count(ff => ff.CreationDate.Month == now.Month && ff.CreationDate.Day == now.Day);
            if (todayCount > 10)
                throw new InvalidOperationException("Todo item daily limit reached");

            context.TodoItems.Add(todoEntity);
            context.SaveChanges();
        }

        public IEnumerable<TodoDto> GetAll()
        {
            var list = context.TodoItems.ToList();
            // refactor to mapper
            return list.Select(ff => new TodoDto
            {
                Id = ff.Id,
                CreationDate = ff.CreationDate,
                IsDone = ff.IsDone,
                Title = ff.Title
            });
        }
    }
}
