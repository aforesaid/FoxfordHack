using System.ComponentModel.DataAnnotations.Schema;

namespace FoxfordHack.Models.Course
{
    public class TaskFoxford
    {
        public int Id { get; set; }
        [ForeignKey("CourseKey")]
        public int CourseId { get; set; }
        public int TaskFoxfordId { get; set; }
        public string Answer { get; set; }
    }
}
