using System.Collections.Generic;

namespace Calculator.Sdk.Model
{
    public class MultiplyDto : IHaveOperationId
    {
        public int? OperationId { get; set; }

        public List<double> Factors { get; set; }
    }
}
