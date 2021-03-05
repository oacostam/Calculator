using System;
using System.Collections.Generic;

namespace Calculator.Sdk.Model
{
    public class OperationDto
    {
        public int? Id { get; set; }

        public string OperationType { get; set; }

        public List<double> Values { get; set; }

        public double Result { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
