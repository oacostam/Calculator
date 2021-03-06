using System.Text.Json.Serialization;

namespace Calculator.Sdk.Model
{
    public class SubtractionDto : IHaveOperationId
    {
        [JsonIgnore]
        public int? OperationId { get; set; }

        public double Minuend { get; set; }

        public double Subtrahend { get; set; }
    }
}
