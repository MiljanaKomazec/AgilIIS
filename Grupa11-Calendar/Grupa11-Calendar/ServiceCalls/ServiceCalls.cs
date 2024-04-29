using Grupa11_Calendar.Models;
using Newtonsoft.Json;

namespace Grupa11_Calendar.ServiceCalls
{
    public class ServiceCalls : IServiceCalls
    {
        private readonly IConfiguration configuration;

        public ServiceCalls(IConfiguration configuration)
        {
            this.configuration = configuration;
        }


        public async Task<List<Calendar>> GetCalendarByUserId(Guid userId)
        {
            using (HttpClient client = new HttpClient())
            {
                var x = configuration["Services:CommentService"];
                Uri url = new Uri($"{configuration["Services:CommentService"]}user/{userId}");

                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Calendar>>(responseBody);
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
