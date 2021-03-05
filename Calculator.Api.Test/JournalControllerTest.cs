using Calculator.Api.Controllers;
using Calculator.Domain.Entities;
using Calculator.Domain.Interfaces;
using Calculator.Sdk.Model;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Calculator.Api.Test
{
    public class JournalControllerTest
    {
        [Fact]
        public async Task ItReturn404IfOpDontExists()
        {
            Mock<IOperationService> operationService = new Mock<IOperationService>();
            operationService.Setup(m => m.GetById(It.IsAny<int>()));
            JournalController journalController = new JournalController(operationService.Object);
            ActionResult<OperationDto> actionResult = await journalController.GetById(1);
            Assert.Equal((int)HttpStatusCode.NotFound, (actionResult.Result as NotFoundResult).StatusCode);
        }


        [Fact]
        public async Task ItReturn200ifOpExists()
        {
            Mock<IOperationService> operationService = new Mock<IOperationService>();
            operationService.Setup(m => m.GetById(It.IsAny<int>())).Returns(Task.FromResult(new Operation() { Id = 1, OperationType = OperationType.Adition }));
            JournalController journalController = new JournalController(operationService.Object);
            ActionResult<OperationDto> actionResult = await journalController.GetById(1);
            var result = actionResult.Result as OkObjectResult;
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(1, ((OperationDto)result.Value).Id);
        }
    }
}
