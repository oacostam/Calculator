using System.Text.Json.Serialization;

namespace Calculator.Sdk.Model
{
    public class SqrtDto : IHaveOperationId
    {
        [JsonIgnore]
        public int? OperationId { get; set; }

        public double Operand { get; set; }
    }
}
