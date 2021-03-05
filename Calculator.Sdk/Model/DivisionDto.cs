namespace Calculator.Sdk.Model
{
    public class DivisionDto : IHaveOperationId
    {
        public int? OperationId { get; set; }

        public double Dividend { get; set; }

        public double Divisor { get; set; }
    }
}
