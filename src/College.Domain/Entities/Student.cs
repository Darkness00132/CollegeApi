using College.Domain.Common;
using College.Domain.Enums;
using College.Domain.ValueObjects;

namespace College.Domain.Entities
{
    public class Student : AggregateRoot
    {
        public Guid UserId { get; set; }
        public AppUser User { get; set; } = null!;

        public Guid ProgramId { get; set; }
        public CollegeProgram Program { get; set; } = null!;

        public string? StudentCode { get; set; }

        public AcademicYear AdmissionAcademicYear { get; set; } = null!;

        public AcademicLevel CurrentLevel { get; set; }
            = AcademicLevel.Of(1);

        public CumulativeGPA Cgpa { get; set; } = null!;

        public AcademicStatus AcademicStatus { get; set; }

        public SecondaryEducationType SecondaryEducationType { get; set; }

        public bool MedicalExamCompleted { get; set; }

        public MilitaryEducationStatus MilitaryEducationStatus { get; set; }
            = MilitaryEducationStatus.NotRequired;
    }
}
