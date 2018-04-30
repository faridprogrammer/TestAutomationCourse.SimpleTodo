using Microsoft.EntityFrameworkCore;
using Phase2.SimpleTodo.Web.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAutomationCourse.SimpleTodo.Web.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {

        }
        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
