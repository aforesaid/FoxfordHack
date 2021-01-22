using System.ComponentModel.DataAnnotations.Schema;

namespace FoxfordHack.Models.DataBaseModels
{
    public class TaskFoxford
    {
        public int Id { get; set; }
        public int TaskFoxfordId { get; set; }
        public string Answer { get; set; }
        [ForeignKey("LessonKey")]
        public int LessonId { get; set; }
    }
}
