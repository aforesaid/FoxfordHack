using FoxfordHack.Models.ModelParsingToJson;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System;
using FoxfordHack.Models.ModelParsingToJson.Question;
namespace FoxfordHack.Services.WebApi.Query
{
    class FoxfordTaskWebService : BaseQuery
    {
        private static readonly string DefaultURLForTasks = @"https://foxford.ru/lessons/";
        public FoxfordTaskWebService(string cookie,string token, int countThreads = 10, int delay = 500)
        {
            Cookie = cookie;
            CountThreads = countThreads;
            Delay = delay;
            XCSRFToken = token;
        }
        public async Task<List<TaskFoxford>> GetTasksByLesson(int lessonId)
        {
            var url = $"{DefaultURL}/api/lessons/{lessonId}/tasks";
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Cookie", Cookie);
            client.DefaultRequestHeaders.Add("User-Agent", DefaultUserAgent);
            var request = await client.GetAsync(url);
            var jsonString = await request.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<List<TaskFoxford>>(jsonString);
            return result;
        }
        public async Task<ViewQuestionPage> GetAnswerFromTask(int lessonId, int taskId,string status)
        {
            var url = $"{DefaultURL}/api/lessons/{lessonId}/tasks/{taskId}";

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Cookie", Cookie);
            client.DefaultRequestHeaders.Add("User-Agent", DefaultUserAgent);
            client.DefaultRequestHeaders.Add("X-CSRF-Token", XCSRFToken);
            if (status != "fail")
            {
                //Fails task
                var request = await client.PostAsync($"{url}/fails", null);
                if (request.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine($"\nRequest Status Code is {request.StatusCode}");
                }
            }
            //Get answers from task
            var requests = await client.GetAsync(url);
            var jsonString = await requests.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ViewQuestionPage>(jsonString);
            return result;
        }
        public async Task<bool> SetAnswerForTask(int lessonId, int taskId,Dictionary<string,string> answer)
        {
            var url = $"{DefaultURLForTasks}{lessonId}/tasks/{taskId}/answer_attempts";
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Cookie", Cookie);
            client.DefaultRequestHeaders.Add("User-Agent", DefaultUserAgent);
            client.DefaultRequestHeaders.Add("XCSRFToken", XCSRFToken);
            var content = new FormUrlEncodedContent(answer);
            var request = await client.PostAsync(url, content);
            if (request.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return false;
            }
            return true;
        }
    }
}
