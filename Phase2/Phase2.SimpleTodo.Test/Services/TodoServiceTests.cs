using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAutomationCourse.SimpleTodo.Web.Data;
using TestAutomationCourse.SimpleTodo.Web.Dto;
using TestAutomationCourse.SimpleTodo.Web.Services;

namespace Phase2.SimpleTodo.Test.Services
{


    [TestFixture]
    public class TodoServiceTests
    {
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

                var service = new TodoService(context, new StubNotificationService());
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
        public void AddTodo_SendsNotification()
        {
            // explain data. this test may fail if we ran the AddTodo_ItemIs11thItemForToday_ExceptionThrown test
            var now = DateTime.Now;
            var unique = Guid.NewGuid();
            var builder = new DbContextOptionsBuilder<DataContext>().UseSqlServer("Server=.;Database=SimpleTodo;Trusted_Connection=True;MultipleActiveResultSets=true");
            var mockNotifyService = new MockNotificationService();

            // explain this deletion
            using (var context = new DataContext(builder.Options))
            {
                context.TodoItems.RemoveRange(context.TodoItems);
                context.SaveChanges();
            }
            // arrange
            using (var context = new DataContext(builder.Options))
            {

                var service = new TodoService(context, mockNotifyService);
                service.AddTodo(new TodoDto
                {
                    Title = "AddTodo_SendsNotification_" + unique,
                    DueDate = DateTime.Now.AddDays(1),
                });
            }

            // assert
            Assert.IsTrue(mockNotifyService.NotifyAddTodoItem_CalledOnce);
        }

    }
}
