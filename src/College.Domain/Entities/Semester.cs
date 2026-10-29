using College.Domain.Common;
using College.Domain.Enums;
using College.Domain.ValueObjects;

namespace College.Domain.Entities
{
    public class Semester : AggregateRoot
    {
        public AcademicYear AcademicYear { get; set; } = null!;

        public SemesterTerm Season { get; set; }
    }
}
