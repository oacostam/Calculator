using Calculator.Domain.Entities;
using Calculator.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calculator.Domain.Services
{
    public class OperationService : IOperationService
    {
        private readonly IRepository<Operation> repository;

        public OperationService(IRepository<Operation> repository)
        {
            this.repository = repository;
        }

        public async Task<Operation> Add(int? operationId, List<double> args)
        {
            Operation operation = new Operation() { Id = operationId.HasValue ? operationId.Value : 0, OperationType = OperationType.Adition, Values = args, Result = args.Sum() };
            if (operationId.HasValue)
            {
                //Concurrency strategy is last one win
                await repository.UpsertOneAsync(operation);
            }
            return operation;
        }

        public async Task<Operation> Div(int? operationId, double dividen, double divisor)
        {
            Operation operation = new Operation() { Id = operationId.HasValue ? operationId.Value : 0, OperationType = OperationType.Division, Values = new List<double>() { dividen, divisor }, Result = dividen / divisor };
            if (operationId.HasValue)
            {
                //Concurrency strategy is last one win
                await repository.UpsertOneAsync(operation);
            }
            return operation;
        }

        public async Task<Operation> Mult(int? operationId, List<double> factors)
        {
            Operation operation = new Operation() { Id = operationId.HasValue ? operationId.Value : 0, OperationType = OperationType.Multiply, Values = factors, Result = factors.Aggregate((a, x) => a * x) };
            if (operationId.HasValue)
            {
                //Concurrency strategy is last one win
                await repository.UpsertOneAsync(operation);
            }
            return operation;
        }

        public async Task<Operation> Sqrt(int? operationId, double operand)
        {
            Operation operation = new Operation() { Id = operationId.HasValue ? operationId.Value : 0, OperationType = OperationType.Square, Values = new List<double>(1) { operand }, Result = Math.Sqrt(operand) };
            if (operationId.HasValue)
            {
                //Concurrency strategy is last one win
                await repository.UpsertOneAsync(operation);
            }
            return operation;
        }

        public async Task<Operation> Sub(int? operationId, double minuend, double subtrahend)
        {
            Operation operation = new Operation() { Id = operationId.HasValue ? operationId.Value : 0, OperationType = OperationType.Subtraction, Values = new List<double>(2) { minuend, subtrahend }, Result = minuend - subtrahend };
            if (operationId.HasValue)
            {
                //Concurrency strategy is last one win
                await repository.UpsertOneAsync(operation);
            }
            return operation;
        }

        public async Task<Operation> GetById(int id)
        {
            return await repository.FindByIdAsync(id);
        }
    }
}
