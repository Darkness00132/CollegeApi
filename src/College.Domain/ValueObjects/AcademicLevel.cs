using College.Domain.Exceptions;

namespace College.Domain.ValueObjects
{
    public class AcademicLevel
    {
        public int Value { get; }

        private AcademicLevel(int value)
        {
            Value = value;
        }

        public static AcademicLevel Of(int value)
        {
            if (value < 1)
                throw new DomainException("Academic level must be at least 1.");

            return new AcademicLevel(value);
        }

        public override string ToString() => Value.ToString();
    }
}
