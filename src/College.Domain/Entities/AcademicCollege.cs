using College.Domain.Common;
using System.Collections.ObjectModel;

namespace College.Domain.Entities
{
    public class AcademicCollege : AggregateRoot
    {
        public string Name { get; set; } = null!;

        public string Code { get; set; } = null!;

        public Collection<Department> Departments { get; set; } = new();
    }
}
