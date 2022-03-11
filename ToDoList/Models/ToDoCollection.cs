using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class ToDoCollection
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        public List<ToDo>? ToDoList { get; set; }
    }
}
