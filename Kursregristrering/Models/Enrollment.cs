namespace Kursregristrering.Models
{
    public class Enrollment
    {
        //primärnyckel
        public int EnrollmentId { get; set; }

        //främmande nycklar
        public int CourseId { get; set; }
        public Course Course { get; set; }

        //främmande nycklar
        public int UserId { get; set; }
        public User User { get; set; }

        //Datum för när Enrollment skapades
        public DateTime EnrollmentDate { get; set; }
    }
}
