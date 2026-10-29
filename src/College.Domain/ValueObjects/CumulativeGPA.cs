namespace College.Domain.ValueObjects
{
    public sealed record CumulativeGPA
    {
        public decimal Value { get; }

        public decimal MaximumValue { get; }

        private CumulativeGPA(decimal value, decimal maximumValue)
        {
            Value = value;
            MaximumValue = maximumValue;
        }

        public static CumulativeGPA Of(
            decimal value,
            decimal maximumValue)
        {
            if (maximumValue <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(maximumValue),
                    "Maximum GPA must be positive.");

            if (value < 0 || value > maximumValue)
                throw new ArgumentOutOfRangeException(
                    nameof(value),
                    $"GPA must be between 0 and {maximumValue}.");

            return new CumulativeGPA(value, maximumValue);
        }

        public override string ToString() => $"{Value}/{MaximumValue}";
    }
}
