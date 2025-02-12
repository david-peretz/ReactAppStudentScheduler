namespace ReactAppStudentScheduler.Server.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> SelectedCourses { get; set; }
    }

}
