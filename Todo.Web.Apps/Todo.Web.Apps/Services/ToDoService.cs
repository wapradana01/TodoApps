using Microsoft.AspNetCore.Components;
using Todo.Ui.Apps.Dtos;

namespace Todo.Ui.Apps.Services
{
    public class ToDoService
    {
        private readonly HttpClient _httpClient;
        [Inject]
        private ApiEndpoints ApiEndpoints { get; set; }
        public ToDoService(HttpClient httpClient, ApiEndpoints apiEndpoints)
        {
            _httpClient = httpClient;
            ApiEndpoints = apiEndpoints;
        }

        public async Task<List<ActivityDto>> GetToDoListAsync(string userId)
        {

            return await _httpClient.GetFromJsonAsync<List<ActivityDto>>($"{ApiEndpoints.ToDo}/{userId}");
        }

        public async Task CreateToDoAsync(ActivityDto toDo)
        {
            await _httpClient.PostAsJsonAsync(ApiEndpoints.ToDo, toDo);
        }

        public async Task EditToDoAsync(string userId, ActivityDto toDo)
        {
            await _httpClient.PutAsJsonAsync($"{ApiEndpoints.ToDo}/edit/{userId}", toDo);
        }

        public async Task MarkToDoAsync(string userId, string activityId)
        {
            await _httpClient.PutAsync($"{ApiEndpoints.ToDo}/mark/{userId}", new StringContent(activityId));
        }

        public async Task DeleteToDoAsync(string userId, string activityId)
        {
            await _httpClient.DeleteAsync($"{ApiEndpoints.ToDo}/delete/{userId}");
        }
    }
}
