using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestAutomationCourse.SimpleTodo.Web.Controllers.Model
{
    public class CreateTodoItemModel
    {
        [Required]
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
