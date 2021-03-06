using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Calculator.Sdk.Model
{
    public class MultiplyDto : IHaveOperationId
    {
        [JsonIgnore]
        public int? OperationId { get; set; }

        public List<double> Factors { get; set; }
    }
}
