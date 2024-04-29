using Newtonsoft.Json;
using Sprint.Models;
using Sprint.Models.ModelBacklog;

namespace Sprint.ServiceCalls
{
    public class ServiceCalls : IServiceCalls
    {
        private readonly IConfiguration configuration;

        public ServiceCalls(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<List<UserStoryDTO>> GetUserStoriesByBacklogId(Guid backlogId)
        {
            using (HttpClient client = new HttpClient())
            {
                var x = configuration["Services:UserStoryService"];
                Uri url = new Uri($"{configuration["Services:UserStoryService"]}backlog/{backlogId}");

                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<UserStoryDTO>>(responseBody);
                }
                else
                {
                    return null;
                }
            }


        }

        public async Task<List<UserStoryDTO>> GetUserStoriesBySprintId(Guid sprintId)
        {
            using (HttpClient client = new HttpClient())
            {
                var x = configuration["Services:UserStoryService"];
                Uri url = new Uri($"{configuration["Services:UserStoryService"]}sprint/{sprintId}");

                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<UserStoryDTO>>(responseBody);
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<List<FunctionalityDTO>> GetFunctionalityBySprintId(Guid sprintId)
        {
            using (HttpClient client = new HttpClient())
            {
                var x = configuration["Services:UserStoryService"];
                Uri url = new Uri($"{configuration["Services:UserStoryService"]}functionality/sprint/{sprintId}");

                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<FunctionalityDTO>>(responseBody);
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<List<TaskDTO>> GetTaskBySprintId(Guid sprintId)
        {
            using (HttpClient client = new HttpClient())
            {
                var x = configuration["Services:UserStoryService"];
                Uri url = new Uri($"{configuration["Services:UserStoryService"]}functionality/task/sprint/{sprintId}");

                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<TaskDTO>>(responseBody);
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<List<TechnicalDebtDTO>> GetTdBySprintId(Guid sprintId)
        {
            using (HttpClient client = new HttpClient())
            {
                var x = configuration["Services:TechnicalDebtService"];
                Uri url = new Uri($"{configuration["Services:TechnicalDebtService"]}sprint/{sprintId}");

                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<TechnicalDebtDTO>>(responseBody);
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
