using System.Text.Json.Serialization;

namespace Calculator.Sdk.Model
{
    public interface IHaveOperationId   
    {
        [JsonIgnore]
        int? OperationId { get; set; }
    }
}
