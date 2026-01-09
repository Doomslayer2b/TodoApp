using AutoMapper;
using TodoApp.Models;
namespace TodoApp.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Todo, TodoItemDTO>();
            CreateMap<TodoItemDTO, Todo>();

        }

       
    }
}
