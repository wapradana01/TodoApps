using AutoMapper;
using Microsoft.Extensions.Logging;
using Todo.Api.DataAccess;

namespace Todo.Api.Core.Services
{
    public class BaseService<T>(ApplicationDbContext dbContext, IMapper mapper, ILogger<T> logger)
    {
        protected readonly ApplicationDbContext _dbContext = dbContext;
        protected readonly IMapper _mapper = mapper;
        protected readonly ILogger<T> _logger = logger;
    }
}
