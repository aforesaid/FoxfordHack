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
            => context.Courses.Include(item=>item.Lessons).ThenInclude(item => item.Tasks).ToList();
        public async Task<ICollection<TaskFoxford>> GetActiveTasksForCourse(int courseId,int lessonId)
            => (await context.Courses.Include(item => item.Lessons)
                                     .ThenInclude(item => item.Tasks)
                                     .FirstOrDefaultAsync(item => item.Id == courseId))
                                     .Lessons.FirstOrDefault(item => item.LessonId == lessonId)
                                     .Tasks;
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
        public async Task<ICollection<Lesson>> GetActiveLessonsByCourse(int courseId)
        {
            var course = await context.Courses
                                      .Include(item => item.Lessons).ThenInclude(item => item.Tasks)
                                      .FirstOrDefaultAsync(item => item.CourseId == courseId);
            return course?.Lessons;
        }
        public async Task<bool> SetActiveLessonsByCourse(List<Lesson> lessons, int courseId)
        {
            try
            {
                var course = await context.Courses.FirstOrDefaultAsync(item => item.CourseId == courseId);
                course?.Lessons.AddRange(lessons);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                new Services.Logging().FixTheError(ex);
            }
            return false;
        }
        public async Task<bool> SetAnswerByTask(string answer,int taskId,int lessonId, int courseId)
        {
            try
            {
                var course = await context.Courses?.Include(item => item.Lessons).
                    ThenInclude(item => item.Tasks).
                    FirstOrDefaultAsync(item => item.CourseId == courseId);
               var task = course?.Lessons.FirstOrDefault(item => item.LessonId == lessonId)
                  .Tasks.FirstOrDefault(item => item.TaskFoxfordId == taskId);
                task.Answer = answer;
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                new Services.Logging().FixTheError(ex);
            }
            return false;
        }
        public async Task<bool> SetActiveTasksForLessonByCourse (List<TaskFoxford> tasks, int courseId,int lessonId)
        {
            try
            {
                var course = await context.Courses.Include(item => item.Lessons).FirstOrDefaultAsync(item => item.CourseId == courseId);
                course?.Lessons.FirstOrDefault(item => item.LessonId == lessonId).Tasks.AddRange(tasks);
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
