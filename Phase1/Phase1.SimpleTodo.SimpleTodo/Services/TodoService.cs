using Phase1.SimpleTodo.Web.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAutomationCourse.SimpleTodo.Web.Data;
using TestAutomationCourse.SimpleTodo.Web.Dto;
using TestAutomationCourse.SimpleTodo.Web.Dto.Validators;

namespace Phase1.SimpleTodo.Web.Services
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
            
            var now = DateTime.Now;
            var exists = context.TodoItems.Any(ff => ff.Title.ToLower() == input.Title.ToLower());
            if (exists)
                throw new InvalidOperationException("Duplicate todo item");

            if(string.IsNullOrEmpty(input.Title))
                throw new InvalidOperationException("Title cannot be empty");

            var todayCount = context.TodoItems.Count(ff => ff.CreationDate.Month == now.Month && ff.CreationDate.Day == now.Day);
            if (todayCount > 10)
                throw new InvalidOperationException("Todo item daily limit reached");
            if (input.DueDate < now)
                throw new InvalidOperationException("Invalid due date");
            if ((input.DueDate - now).Days >= 7)
                throw new InvalidOperationException("Cannot set due date to future date more than 7 days later");

            // refactor to mapper
            var todoEntity = new TodoItem
            {
                CreationDate = DateTime.Now,
                Title = input.Title,
                IsDone = false,
                DueDate = input.DueDate
            };

            context.TodoItems.Add(todoEntity);
            context.SaveChanges();

            var notificationService = new NotificationService();
            notificationService.NotifyAddTodoItem();
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
