using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Phase1.SimpleTodo.Web.Domain;
using Phase1.SimpleTodo.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAutomationCourse.SimpleTodo.Web.Data;
using TestAutomationCourse.SimpleTodo.Web.Dto;

namespace Phase1.SimpleTodo.Test.Services
{
    [TestFixture]
    public class TodoServiceTests
    {
        [Test]
        public void AddTodo_TitleIsEmpty_ExceptionThrown()
        {
            var builder = new DbContextOptionsBuilder<DataContext>().UseSqlServer("Server=.;Database=SimpleTodo;Trusted_Connection=True;MultipleActiveResultSets=true");

            using (var context = new DataContext(builder.Options))
            {
                var service = new TodoService(context);
                Assert.Throws<InvalidOperationException>(() => service.AddTodo(new TodoDto
                {
                    Title = "",
                    DueDate = DateTime.Now,
                    IsDone = false
                }));
            }
        }

        [Test]
        public void AddTodo_TitleIsDuplicate_ExceptionThrown()
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
                var service = new TodoService(context);
                Assert.Throws<InvalidOperationException>(() => service.AddTodo(new TodoDto
                {
                    Title = "TitleIsDuplicate_" + unique,
                    DueDate = DateTime.Now,
                    IsDone = false
                }));
            }
        }

        [Test]
        public void AddTodo_DefaultIsDone_False()
        {
            // explain data. this test will fail if we ran the AddTodo_ItemIs11thItemForToday_ExceptionThrown test
            var now = DateTime.Now;
            var unique = Guid.NewGuid();
            var builder = new DbContextOptionsBuilder<DataContext>().UseSqlServer("Server=.;Database=SimpleTodo;Trusted_Connection=True;MultipleActiveResultSets=true");


            // act
            using (var context = new DataContext(builder.Options))
            {
                var service = new TodoService(context);
                service.AddTodo(new TodoDto
                {
                    Title = "DefaultIsDone_" + unique,
                    DueDate = DateTime.Now,
                });
            }

            // arrange data
            using (var context = new DataContext(builder.Options))
            {
                Assert.IsFalse(context.TodoItems.Single(ff => ff.Title == "DefaultIsDone_" + unique).IsDone);
            }

        }


        [Test]
        public void AddTodo_ItemIs11thItemForToday_ExceptionThrown()
        {
            var now = DateTime.Now;
            var builder = new DbContextOptionsBuilder<DataContext>().UseSqlServer("Server=.;Database=SimpleTodo;Trusted_Connection=True;MultipleActiveResultSets=true");

            // arrange data
            using (var context = new DataContext(builder.Options))
            {
                for (int i = 0; i <= 10; i++)
                {
                    context.TodoItems.Add(new TodoItem
                    {
                        DueDate = now.AddDays(3),
                        IsDone = false,
                        Title = "ItemIs11thItemForToday_" + i,
                        CreationDate = now
                    });
                }
                context.SaveChanges();
            }

            // act
            using (var context = new DataContext(builder.Options))
            {
                var service = new TodoService(context);
                Assert.Throws<InvalidOperationException>(() => service.AddTodo(new TodoDto
                {
                    Title = "ItemIs11thItemForToday_" + Guid.NewGuid(),
                    DueDate = DateTime.Now,
                    IsDone = false
                }));
            }
        }
    }
}
