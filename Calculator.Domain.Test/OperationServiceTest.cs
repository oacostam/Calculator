using Calculator.Domain.Entities;
using Calculator.Domain.Interfaces;
using Calculator.Domain.Services;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Calculator.Domain.Test
{
    public class OperationServiceTest
    {

        [Fact]
        public async Task ItAddManyNumbers()
        {
            Mock<IRepository<Operation>> repository = new Mock<IRepository<Operation>>();
            OperationService operationService = new OperationService(repository.Object);
            Operation result = await operationService.Add(null, new List<double>() { 1, 2, 3 });
            Assert.Equal(OperationType.Adition, result.OperationType);
            Assert.Equal(6, result.Result);
        }

        [Fact]
        public async Task ItAMultiplyManyNumbers()
        {
            Mock<IRepository<Operation>> repository = new Mock<IRepository<Operation>>();
            OperationService operationService = new OperationService(repository.Object);
            Operation result = await operationService.Mult(null, new List<double>() { 2, 3, 2 });
            Assert.Equal(OperationType.Multiply, result.OperationType);
            Assert.Equal(12, result.Result);
        }


        [Fact]
        public async Task ItASubtractTwoNumbers()
        {
            Mock<IRepository<Operation>> repository = new Mock<IRepository<Operation>>();
            OperationService operationService = new OperationService(repository.Object);
            Operation result = await operationService.Sub(null, 10, 1);
            Assert.Equal(OperationType.Subtraction, result.OperationType);
            Assert.Equal(9, result.Result);
        }

        [Fact]
        public async Task ItADivideTwoNumbers()
        {
            Mock<IRepository<Operation>> repository = new Mock<IRepository<Operation>>();
            OperationService operationService = new OperationService(repository.Object);
            Operation result = await operationService.Div(null, 10, 2);
            Assert.Equal(OperationType.Division, result.OperationType);
            Assert.Equal(5, result.Result);
        }

        [Fact]
        public async Task ItComputesSquareRoot()
        {
            Mock<IRepository<Operation>> repository = new Mock<IRepository<Operation>>();
            OperationService operationService = new OperationService(repository.Object);
            Operation result = await operationService.Sqrt(null, 16);
            Assert.Equal(OperationType.Square, result.OperationType);
            Assert.Equal(4, result.Result);
        }


        [Fact]
        public async Task ItSaveOperationWhenIdPresent()
        {
            List<Operation> db = new List<Operation>();
            Mock<IRepository<Operation>> repository = new Mock<IRepository<Operation>>();
            repository.Setup(m => m.UpsertOneAsync(It.IsAny<Operation>())).Callback((Operation o) => db.Add(o));
            OperationService operationService = new OperationService(repository.Object);
            await operationService.Add(1, new List<double>() { 1, 2, 3 });
            Operation existing = db[0];
            Assert.Equal(1, existing.Id);
            Assert.Equal(OperationType.Adition, existing.OperationType);
            Assert.Equal(6, existing.Result);
        }
    }
}
