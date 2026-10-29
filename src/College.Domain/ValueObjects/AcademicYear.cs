using College.Domain.Exceptions;

namespace College.Domain.ValueObjects
{
    public sealed record AcademicYear
    {
        public int StartYear { get; }

        public int EndYear => StartYear + 1;

        private AcademicYear(int startYear)
        {
            StartYear = startYear;
        }

        public static AcademicYear Of(int startYear)
        {
            if (startYear < 2000)
                throw new DomainException(
                    "Academic year cannot start before 2000.");

            return new AcademicYear(startYear);
        }

        public override string ToString() => $"{StartYear}/{EndYear}";
    }
}
