using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace TodoApp.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public ICollection<Todo> Todos { get; set; } = new List<Todo>();
    }
}
