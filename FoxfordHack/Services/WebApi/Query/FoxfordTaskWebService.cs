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
        public FoxfordTaskWebService(string cookie, string token, int countThreads = 10, int delay = 500)
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
        public async Task<ViewQuestionPage> GetAnswerFromTask(TaskFoxford task, int lessonId)
        {
            if (task is null)
                return null;
            var url = $"{DefaultURL}/api/lessons/{lessonId}/tasks/{task.Id}";
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Cookie", Cookie);
            client.DefaultRequestHeaders.Add("User-Agent", DefaultUserAgent);
            client.DefaultRequestHeaders.Add("X-CSRF-Token", XCSRFToken);
            var requests = await client.GetAsync(url);
            var jsonString = await requests.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ViewQuestionPage>(jsonString);
            return result;
        }
        public void FailsAnswerFromTaskByLesson(List<TaskFoxford> listTasks, int lessonId)
        {
            if (listTasks is null)
                return;
            var tasks = new Task[listTasks.Count];
            for (int i = 0; i < CountThreads; i++)
            {
                tasks[i] = FailsAnswerFromTasks(listTasks, lessonId, i);
            }
            Task.WaitAll(tasks);
        }
        private async Task FailsAnswerFromTasks(List<TaskFoxford> tasks, int lessonId,int startIndex)
        {
            for (int i = startIndex; i < tasks.Count; i+= CountThreads)
            {
                await FailsAnswerFromTask(tasks[i],lessonId);
            }
        }
        public async Task<bool> FailsAnswerFromTask(TaskFoxford task,int lessonId)
        {
            if (task.Status is "fail")
                return false;
            var url = $"{DefaultURL}/api/lessons/{lessonId}/tasks/{task.Id}/fails";
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Cookie", Cookie);
            client.DefaultRequestHeaders.Add("User-Agent", DefaultUserAgent);
            client.DefaultRequestHeaders.Add("X-CSRF-Token", XCSRFToken);
            var request = await client.PostAsync(url, null);
            if (request.StatusCode != System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine($"Status Code is {request.StatusCode},Links {url}");
                await FailsAnswerFromTask(task, lessonId);
            }
            task.Status = "fail";
            await Task.Delay(Delay);
            return true;
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
