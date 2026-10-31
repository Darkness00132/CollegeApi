using College.Domain.Common;

namespace College.Domain.Entities.Affairs
{
    public class AcademicAdvisorAssignment : Entity
    {
        public Guid StudentId { get; set; }
        public Student Student { get; set; } = null!;

        public Guid ProfessorId { get; set; }
        public ProfessorProfile Professor { get; set; } = null!;
    }
}
