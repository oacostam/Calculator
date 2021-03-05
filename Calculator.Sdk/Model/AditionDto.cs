using System.Collections.Generic;

namespace Calculator.Sdk.Model
{
    public class AditionDto : IHaveOperationId
    {
        public int? OperationId { get; set; }

        public List<double> Addends { get; set; }
    }
}
