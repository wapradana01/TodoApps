using AutoMapper;
using Todo.Api.Core.Services.Core.Dtos;
using Todo.Api.Core.Services.Core.Inputs;
using Todo.Api.DataAccess.Models;

namespace Todo.Api.Core.Services.Core.Configurations
{
    public class ActivityMapperConfiguration : Profile
    {
        public ActivityMapperConfiguration()
        {
            CreateMap<TodoActivity, ActivityDto>();
        }
    }
}
