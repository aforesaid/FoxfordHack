using FoxfordHack.Models.Course;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace FoxfordHack.DataBase
{
    interface IRepository : IDisposable, IAsyncDisposable
    {
        public ICollection<Course> GetActiveCourse();
        public  Task<ICollection<Models.Course.TaskFoxford>> GetActiveTasksForCourse(int courseId);
        public  Task<bool> SetActiveCourse(ICollection<Course> IdActiveCourse);
        public  Task<bool> SetActiveTasksForCourse(ICollection<Models.Course.TaskFoxford> tasks, int courseId);
    }
}
