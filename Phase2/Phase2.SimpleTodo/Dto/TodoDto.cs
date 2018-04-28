using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestAutomationCourse.SimpleTodo.Web.Dto
{
    public class TodoDto
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public bool IsDone { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
