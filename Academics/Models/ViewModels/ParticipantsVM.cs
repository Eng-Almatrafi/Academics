namespace Academics.Models.ViewModels
{
    public class ParticipantsVM
    {
        public Individual? Individual { get; set; }
        public Corporate? Corporate { get; set; }
        public Enrollment? Enrollment { get; set; }
        public Section? Section { get; set; }
        public ICollection<Section> Sections { get; set; }= new List<Section>();


    }
}
