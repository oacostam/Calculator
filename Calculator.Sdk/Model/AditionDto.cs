using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Calculator.Sdk.Model
{
    public class AditionDto : IHaveOperationId
    {
        [JsonIgnore]
        public int? OperationId { get; set; }

        public List<double> Addends { get; set; }
    }
}
