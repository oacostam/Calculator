namespace Calculator.Sdk.Model
{
    public class SqrtDto : IHaveOperationId
    {
        public int? OperationId { get; set; }

        public double Operand { get; set; }
    }
}
