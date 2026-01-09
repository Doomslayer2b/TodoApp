using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models
{
    public class TodoItemDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        [Range(1, int.MaxValue)]
        public int CategoryId { get; set; }

        public TodoItemDTO() { } // <-- parameterless constructor

        public TodoItemDTO(Todo todo)
        {
            Id = todo.Id;
            Name = todo.Name;
            IsComplete = todo.IsComplete;
            CategoryId = todo.CategoryId;
        }
    }

}

