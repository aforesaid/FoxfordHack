using System.Collections.Generic;

namespace FoxfordHack.Models.DataBaseModels
{
    public class Course
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Name { get; set; }
        public List<TaskFoxford> Tasks { get; set; } = new List<TaskFoxford>();
    }
}
