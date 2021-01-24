using FoxfordHack.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace FoxfordHack.DataBase
{
    interface IRepository : IDisposable, IAsyncDisposable
    {
        public ICollection<Course> GetActiveCourse();
        public  Task<ICollection<TaskFoxford>> GetActiveTasksForCourse(int courseId, int lessonId);
        public  Task<bool> SetActiveCourse(ICollection<Course> IdActiveCourse);
        public  Task<bool> SetActiveTasksForLessonByCourse(List<TaskFoxford> tasks, int courseId,int lessonId);
        public  Task<bool> SetActiveLessonsByCourse(List<Lesson> lessons, int courseId);
        public Task<ICollection<Lesson>> GetActiveLessonsByCourse(int courseId);
        public Task<bool> SetAnswerByTask(string answer, int taskId, int lessonId, int courseId);

    }
}
