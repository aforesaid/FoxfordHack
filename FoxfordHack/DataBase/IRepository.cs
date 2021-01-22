using FoxfordHack.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace FoxfordHack.DataBase
{
    interface IRepository : IDisposable, IAsyncDisposable
    {
        public ICollection<Course> GetActiveCourse();
        public  Task<ICollection<Models.DataBaseModels.TaskFoxford>> GetActiveTasksForCourse(int courseId);
        public  Task<bool> SetActiveCourse(ICollection<Course> IdActiveCourse);
        public  Task<bool> SetActiveTasksForCourse(ICollection<Models.DataBaseModels.TaskFoxford> tasks, int courseId);
    }
}
