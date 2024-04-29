using Newtonsoft.Json;
using StoryPointAPI.DTO;
using UserStory.Models.ModelUserStory;

namespace UserStory.ServiceCalls
{
    public class ServiceCalls : IServiceCalls
    {
        private readonly IConfiguration configuration;

        public ServiceCalls(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        //veza sa Comment
        public async Task<List<CommentDTO>> GetCommentByUserStoryId(Guid userStoryRootId)
        {
            using (HttpClient client = new HttpClient())
            {
                var x = configuration["Services:CommentService"];
                Uri url = new Uri($"{configuration["Services:CommentService"]}userStory/{userStoryRootId}");

                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<CommentDTO>>(responseBody);
                }
                else
                {
                    return null;
                }
            }
        }

        //veza sa StoryPoint
        public async Task<List<StoryPointDTO>> GetStoryPointsByUserStoryId(Guid userStoryRootId)
        {
            using (HttpClient client = new HttpClient())
            {
                var x = configuration["Services:StoryPointService"];
                Uri url = new Uri($"{configuration["Services:StoryPointService"]}userStory/{userStoryRootId}");

                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<StoryPointDTO>>(responseBody);
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
