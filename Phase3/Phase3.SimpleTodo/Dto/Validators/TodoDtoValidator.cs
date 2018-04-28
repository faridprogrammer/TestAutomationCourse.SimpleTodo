using System;
using System.Linq;
using TestAutomationCourse.SimpleTodo.Web.Data;

namespace TestAutomationCourse.SimpleTodo.Web.Dto.Validators
{
    public class TodoDtoValidator
    {
        private readonly DataContext context;

        public TodoDtoValidator(DataContext context)
        {
            this.context = context;
        }
        public void Validate(TodoDto input)
        {
            // refactor to validator or guard
            var now = DateTime.Now;
            var todayCount = context.TodoItems.Count(ff => ff.CreationDate.Month == now.Month && ff.CreationDate.Day == now.Day);
            if (todayCount > 10)
                throw new InvalidOperationException("Todo item daily limit reached");
            if (input.DueDate < now)
                throw new InvalidOperationException("Invalid due date");
            if ((input.DueDate - now).Days >= 7)
                throw new InvalidOperationException("Cannot set due date to future date more than 7 days later");
        }

    }
}
