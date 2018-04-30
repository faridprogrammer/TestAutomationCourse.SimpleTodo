using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Phase2.SimpleTodo.Web.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAutomationCourse.SimpleTodo.Web.Data;
using TestAutomationCourse.SimpleTodo.Web.Dto;
using TestAutomationCourse.SimpleTodo.Web.Dto.Validators;
using TestAutomationCourse.SimpleTodo.Web.Services;

namespace Phase2.SimpleTodo.Test.Services
{
    [TestFixture]
    public class TodoValidatorTests
    {
        [Test]
        public void Validate_TitleIsEmpty_ExceptionThrown() {
            var builder = new DbContextOptionsBuilder<DataContext>().UseSqlServer("Server=.;Database=SimpleTodo;Trusted_Connection=True;MultipleActiveResultSets=true");

            using (var context = new DataContext(builder.Options))
            {

                var validator = new TodoDtoValidator(context);
                Assert.Throws<InvalidOperationException>(() => validator.Validate(new TodoDto
                {
                    Title = "",
                    DueDate = DateTime.Now,
                    IsDone = false
                }));
            }
        }

        [Test]
        public void Validate_TitleIsDuplicate_ExceptionThrown()
        {
            var now = DateTime.Now;
            var unique = Guid.NewGuid();
            var builder = new DbContextOptionsBuilder<DataContext>().UseSqlServer("Server=.;Database=SimpleTodo;Trusted_Connection=True;MultipleActiveResultSets=true");

            // arrange data
            using (var context = new DataContext(builder.Options))
            {
                context.TodoItems.Add(new TodoItem
                {
                    DueDate = now.AddDays(3),
                    IsDone = false,
                    Title = "TitleIsDuplicate_" + unique
                });
                context.SaveChanges();
            }

            // act
            using (var context = new DataContext(builder.Options))
            {
                var validator = new TodoDtoValidator(context);
                Assert.Throws<InvalidOperationException>(() => validator.Validate(new TodoDto
                {
                    Title = "TitleIsDuplicate_" + unique,
                    DueDate = DateTime.Now,
                    IsDone = false
                }));
            }
        }
    }
}
