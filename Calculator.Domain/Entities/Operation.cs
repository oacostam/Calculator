using System;
using System.Collections.Generic;

namespace Calculator.Domain.Entities
{
    public class Operation : EntityBase
    {

        public Operation()
        {
            CreationDate = DateTime.UtcNow;
        }


        public OperationType OperationType { get; set; }

        public List<double> Values { get; set; }

        public double Result { get; set; }

        public DateTime CreationDate { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Operation operation &&
                   OperationType == operation.OperationType &&
                   EqualityComparer<List<double>>.Default.Equals(Values, operation.Values) &&
                   Result == operation.Result &&
                   CreationDate == operation.CreationDate;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(OperationType, Values, Result, CreationDate);
        }
    }
}
