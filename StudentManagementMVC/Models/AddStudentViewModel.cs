namespace StudentManagementMVC.Models
{
    public class AddStudentViewModel
    {
        public Guid? ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool Subed { get; set; }
    }
}
