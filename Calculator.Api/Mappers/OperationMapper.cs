using Calculator.Domain.Entities;
using Calculator.Sdk.Model;
using System;

namespace Calculator.Api.Mappers
{
    public static class OperationMapper
    {
        public static OperationDto Map(Operation operation)
        {
            // In a real world scenario, translation between domain model and DTO's would be donne using a tool like Automapper.
            OperationDto operationDto = new OperationDto()
            {
                CreationDate = operation.CreationDate,
                Id = operation.Id == 0 ? new int?() : operation.Id,
                OperationType = Enum.GetName(typeof(OperationType), operation.OperationType),
                Values = operation.Values,
                Result = operation.Result
            };
            return operationDto;
        }
    }
}
