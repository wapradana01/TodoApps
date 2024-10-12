using Microsoft.AspNetCore.Mvc;
using Todo.Api.Core.Services.Core;
using Todo.Api.Core.Services.Core.Dtos;
using Todo.Api.Core.Services.Core.Inputs;
using Todo.Api.Shared.Attributes;
using Todo.Api.Shared.Objects;
using Todo.Api.Shared.Objects.Dtos;
using Todo.Api.Shared.Objects.Inputs;

namespace Todo.Api.Apps.Controllers
{
    [Route("api/activity")]
    public class ActivityController(ActivityService activityService, CurrentUserAccessor currentUserAccessor) : BaseController
    {
        private readonly CurrentUserAccessor _currentUserAccessor = currentUserAccessor;
        private readonly ActivityService _activityService = activityService;

        [Mutation]
        [HttpPost("create")]
        public async Task<ResponseObject<ActivityDto>> Create([FromBody] List<ActivityInput> input)
        {
            return await _activityService.CreateActivity(input);
        }

        [HttpPost("getlist")]
        public async Task<ResponsePaging<ActivityDto>> GetList([FromBody] PagingSearchInputBase input)
        {
            return await _activityService.GetListActivity(input, _currentUserAccessor);
        }

        [Mutation]
        [HttpPost("delete")]
        public async Task<ResponseObject<object>> Delete([FromBody] Guid id)
        {
            return await _activityService.DeleteActivity(id);
        }

        [Mutation]
        [HttpPost("updatestatus")]
        public async Task<ResponseObject<ActivityDto>> UpdateStatus([FromBody] UpdateActivityStatusInput input)
        {
            return await _activityService.UpdataStatusActivity(input);
        }

        [Mutation]
        [HttpPost("update")]
        public async Task<ResponseObject<ActivityDto>> Update([FromBody] ActivityInput input)
        {
            return await _activityService.UpdataDataActvity(input);
        }
    }
}
