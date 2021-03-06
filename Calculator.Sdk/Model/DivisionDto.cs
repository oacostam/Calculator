using System;
using System.Text.Json.Serialization;

namespace Calculator.Sdk.Model
{
    public class DivisionDto : IHaveOperationId
    {
        private double divisor;

        [JsonIgnore]
        public int? OperationId { get; set; }

        public double Dividend { get; set; }

        public double Divisor 
        { 
            get => divisor;
            set 
            {
                if (value == 0)
                    throw new ArgumentException("Can't divide by 0");
                divisor = value;
            } 
        }
    }
}
