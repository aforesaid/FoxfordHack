using System;
using System.Collections.Generic;
using System.Text;

namespace FoxfordHack.Models.Course
{
    public class Course
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Name { get; set; }
        public List<TaskFoxford> Tasks { get; set; }
    }
}
