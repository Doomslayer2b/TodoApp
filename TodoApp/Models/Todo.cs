using System.ComponentModel.DataAnnotations;
namespace TodoApp.Models
{
    public class Todo
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public string? Secret { get; set; }
        [Range(1, int.MaxValue)]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
