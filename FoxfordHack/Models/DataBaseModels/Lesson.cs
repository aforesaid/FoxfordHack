using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoxfordHack.Models.DataBaseModels
{
    public class Lesson
    {
        public int Id { get; set; }
        [ForeignKey("CourseKey")]
        public int LessonId { get; set; }
        public int CourseId { get; set; }
        public List<TaskFoxford> Tasks { get; set; } = new List<TaskFoxford>();
    }
}
