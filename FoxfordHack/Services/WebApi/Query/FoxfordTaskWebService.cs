using FoxfordHack.Models.ModelParsingToJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace FoxfordHack.Services.WebApi.Query
{
    class FoxfordTaskWebService : BaseQuery
    {
        private static readonly string DefaultURLForTasks = @"https://foxford.ru/lessons/";
        public FoxfordTaskWebService(string cookie, int countThreads = 10, int delay = 500)
        {
            Cookie = cookie;
            CountThreads = countThreads;
            Delay = delay;
        }
        public async Task<TaskFoxford> GetTasksByLesson(int courseId, int lessonId)
        {

        }
    }
}
