﻿using Newtonsoft.Json;
using URIS_Grupa11.Models.OtherModel;

namespace URIS_Grupa11.Helpers
{
    public class LoggerService : ILoggerService
    {
        private readonly IConfiguration configuration;

        public LoggerService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<bool> Log(LogLevel level, string method, string message, Exception exc = null)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    string url = configuration["Services:Logger_Service"];
                    var log = new Logger
                    {
                        ServiceName = "User story Service",
                        LogLevel = level,
                        Method = method,
                        Message = message,
                        Exc = exc ?? new Exception("UnknownError")
                    };

                    HttpContent content = new StringContent(JsonConvert.SerializeObject(log));
                    content.Headers.ContentType.MediaType = "application/json";


                    HttpResponseMessage response = httpClient.PostAsync(url, content).Result;

                    return await Task.FromResult(response.IsSuccessStatusCode);
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
