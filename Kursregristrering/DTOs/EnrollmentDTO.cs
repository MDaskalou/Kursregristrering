namespace Kursregristrering.DTOs
{
    public class EnrollmentDTO
    {
        //unik id för enrollment
        public int EnrollmentId { get; set; }
        //unik id för user
        public Guid UserId { get; set; }
        //unik id för course
        public int CourseId { get; set; }


        //datum för när enrollment skapades

        public DateTime EnrollmentDate { get; set; }
    }
}
