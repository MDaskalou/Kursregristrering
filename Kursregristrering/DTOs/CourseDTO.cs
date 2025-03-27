namespace Kursregristrering.DTOs
{
    public class CourseDTO
    {
        //skapar en DTO för Course
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int points { get; set; }

    }
}
