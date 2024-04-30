namespace Academics.Models.ViewModels
{
    public class SectionVM
    {
        public Section? Section { get; set; }
        public ICollection<Course>  Course { get; set; } = new List<Course>();
        public ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();
        public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    }
}
