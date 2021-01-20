using FoxfordHack.Models.ModelParsingToJson.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
namespace FoxfordHack.Services.WebApi.Query
{
    class CourseWebService : BaseQuery
    {
        private static readonly string DefaultURLForActiveCourses = @"https://foxford.ru/api/user/bookmarks";
        public CourseWebService(string cookie, int countThreads = 10, int delay = 500)
        {
            Cookie = cookie;
            CountThreads = countThreads;
            Delay = delay;
        }
        public void ActivateCourseByPromo(string promo, int minCourseId = 1, int maxCourseId = 10000)
        {
            var tasks = new Task[CountThreads];
            for (int i = 0; i < CountThreads; i++)
            {
                tasks[i] = SendRequestForActiveCourse(i, promo, minCourseId, maxCourseId);
            }
            Task.WaitAll(tasks);
        }
        public async Task<List<Course>> GetAllActiveCourses()
        {
            var result = new List<Course>();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Cookie", Cookie);
                client.DefaultRequestHeaders.Add("User-Agent", DefaultUserAgent);
                var flag = true;
                var index = 1;
                while (flag)
                {
                    var urlContent = $"{DefaultURLForActiveCourses}?page={index}&archived=false";
                    var request = await client.GetAsync(urlContent);
                    var jsonString = await request.Content.ReadAsStringAsync();
                    var list = JsonSerializer.Deserialize<List<Course>>(jsonString);
                    result.AddRange(list);
                    result = result.Distinct().ToList();
                    index++;
                }
            }
            return result;
        }
        private async Task SendRequestForActiveCourse(int position,string promo, int minCourseId = 1, int maxCourseId = 10000)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Cookie", Cookie);
                client.DefaultRequestHeaders.Add("User-Agent", DefaultUserAgent);
                for (int i = minCourseId + position; i < maxCourseId; i+= CountThreads)
                {
                    var urlContent = $"{DefaultURL}/{i}/activate/{promo}";
                    try
                    {
                        var result = await client.GetAsync(urlContent);
                        await Task.Delay(Delay);
                    }
                    catch(Exception ex)
                    {
                        new Logging().FixTheError(ex);
                    }
                }
            }
        }
    }
}
