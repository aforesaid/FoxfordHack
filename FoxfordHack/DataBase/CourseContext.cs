using FoxfordHack.DataBase.DBApplication;
using FoxfordHack.Models.DataBaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace FoxfordHack.DataBase
{
    class CourseContext : IRepository
    {
        private ApplicationDbContext context;
        public CourseContext()
            => context = new ApplicationDbContext();
        public ICollection<Course> GetActiveCourse()
            => context.Courses.ToList();
        public async Task<ICollection<TaskFoxford>> GetActiveTasksForCourse(int courseId)
            => (await context.Courses.Include(item => item.Tasks)
                              .FirstOrDefaultAsync(item => item.Id == courseId)).Tasks;
        public async Task<bool> SetActiveCourse (ICollection<Course> ActiveCourse)
        {
            try
            {
                context.Courses.AddRange(ActiveCourse);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                new Services.Logging().FixTheError(ex);
            }
            return false;
        }
        public async Task<bool> SetActiveTasksForCourse (ICollection<Models.DataBaseModels.TaskFoxford> tasks, int courseId)
        {
            try
            {
                var course = await context.Courses.FirstOrDefaultAsync(item => item.CourseId == courseId);
                course?.Tasks.AddRange(tasks);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                new Services.Logging().FixTheError(ex);
            }
            return false;
        }
        #region Disposable
        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
        #region AsyncDisposable
        public async ValueTask DisposeAsync()
        {
            await DisposeAsyncCore();
            Dispose(false);
            GC.SuppressFinalize(this);
        }
        protected virtual async ValueTask DisposeAsyncCore()
        {
            //Now is empty method
        }
        #endregion
    }
}
