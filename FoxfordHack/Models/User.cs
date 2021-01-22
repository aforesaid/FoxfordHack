using System.Collections.Generic;
namespace FoxfordHack.Models
{
    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; } 
        public List<DataBaseModels.Course> Courses { get; set; }
    }
}
