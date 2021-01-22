using FoxfordHack.Models.ModelParsingToJson.Lesson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace FoxfordHack.Services.WebApi.Query
{
    class FoxfordLessonsWebService : BaseQuery
    {
        private static readonly string DefaultURLForLessons = @"https://foxford.ru/api/courses";
        public FoxfordLessonsWebService(string cookie, int countThreads = 10, int delay = 500)
        {
            Cookie = cookie;
            CountThreads = countThreads;
            Delay = delay;
        }
        public async Task<List<Lesson>> GetAllLessonsInCourse(int courseId)
        {
            var result = new List<Lesson>();
            ViewLesson model = await GetViewLesson(courseId, 0);
            result.AddRange(model.Lessons);
            while (model.Cursor.After != null)
            {
                ViewLesson _model = await GetViewLesson(courseId, (int) model.Cursor.After);
                result.AddRange(_model.Lessons);
                model.Cursor.After = _model.Cursor.After;
            }
            while (model.Cursor.Before != null)
            {
                ViewLesson _model = await GetViewLesson(courseId, (int) model.Cursor.Before);
                result.AddRange(_model.Lessons);
                model.Cursor.Before = _model.Cursor.Before;
            }
            await Task.Delay(Delay);
            return result;
        }
        public async Task<ViewLesson> GetViewLesson(int courseId,int cursor = 0)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Cookie", Cookie);
            client.DefaultRequestHeaders.Add("User-Agent", DefaultUserAgent);
            var queryByUrl = cursor == 0 ? "" : $"?lesson_id={cursor}";
            var urlQuery = $"{DefaultURLForLessons}/{courseId}/lessons{queryByUrl}";
            var request = await client.GetAsync(urlQuery);
            var jsonString = await request.Content.ReadAsStringAsync();
            var model = JsonSerializer.Deserialize<ViewLesson>(jsonString);
            return model;
        }
    }
}
