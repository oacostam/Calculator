namespace Calculator.Sdk.Model
{
    public class SubtractionDto : IHaveOperationId
    {
        public int? OperationId { get; set; }

        public double Minuend { get; set; }

        public double Subtrahend { get; set; }
    }
}
