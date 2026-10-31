using College.Domain.Common;
using System.Collections.ObjectModel;

namespace College.Domain.Entities.Affairs
{
    public class AcademicCollege : AggregateRoot
    {
        public string Name { get; set; } = null!;

        public string Code { get; set; } = null!;

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public Collection<Department> Departments { get; set; } = new();
    }
}
