using Calculator.Domain.Entities;
using System.Collections.Generic;
    using System.Threading.Tasks;

namespace Calculator.Domain.Interfaces
{
    public interface IOperationService
    {
        //TODO: Parameter must be encapsulated in a class
        Task<Operation> Add(int? operationId, List<double> args);

        //TODO: Parameter must be encapsulated in a class
        Task<Operation> Sub(int? operationId, double minuend, double subtrahend);

        //TODO: Parameter must be encapsulated in a class
        Task<Operation> Mult(int? operationId, List<double> factors);

        //TODO: Parameter must be encapsulated in a class
        Task<Operation> Div(int? operationId, double dividen, double divisor);

        //TODO: Parameter must be encapsulated in a class
        Task<Operation> Sqrt(int? operationId, double operand);

        //TODO: Parameter must be encapsulated in a class
        Task<Operation> GetById(int id);

    }
}
