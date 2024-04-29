using Newtonsoft.Json;
using WebApplication5.DTO;

namespace WebApplication5.ServiceCalls
{
    public class ServiceCalls : IServiceCalls
    {
        private readonly IConfiguration configuration;

        public ServiceCalls(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<List<CommentDTO>> GetCommentByUserId(Guid userId)
        {
            using (HttpClient client = new HttpClient())
            {
                var x = configuration["Services:CommentService"];
                Uri url = new Uri($"{configuration["Services:CommentService"]}user/{userId}");

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
    }
}
    
