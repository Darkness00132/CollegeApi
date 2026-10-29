using College.Domain.Common;

namespace College.Domain.Entities
{
    public class Course : AggregateRoot
    {
        public string Code { get; set; } = null!;

        public string Name { get; set; } = null!;

        public int? CreditHours { get; set; }

        public Guid ProgramId { get; set; }
        public CollegeProgram Program { get; set; } = null!;
    }
}
