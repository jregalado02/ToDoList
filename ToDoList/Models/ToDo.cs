using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class ToDo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        public bool IsDone { get; set; }

    }
}
