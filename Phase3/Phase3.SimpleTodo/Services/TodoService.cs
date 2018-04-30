using Phase3.SimpleTodo.Web.Domain;
using Phase3.SimpleTodo.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAutomationCourse.SimpleTodo.Web.Data;
using TestAutomationCourse.SimpleTodo.Web.Dto;
using TestAutomationCourse.SimpleTodo.Web.Dto.Validators;

namespace Phase3.SimpleTodo.Web.Services
{
    public interface ITodoService {
        void AddTodo(TodoDto input);
        IEnumerable<TodoDto> GetAll();
    }
    public class TodoService: ITodoService
    {
        private readonly DataContext context;
        private readonly INotificationService notificationService;

        public TodoService(DataContext context, INotificationService notificationService)
        {
            this.context = context;
            this.notificationService = notificationService;
        }
        public void AddTodo(TodoDto input)
        {
            var exists = context.TodoItems.Any(ff => ff.Title.ToLower() == input.Title.ToLower());
            if (exists)
                throw new InvalidOperationException("Duplicate todo item");

            var validator = new TodoDtoValidator(context);
            validator.Validate(input);

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
