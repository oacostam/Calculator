using Calculator.Api.Mappers;
using Calculator.Domain.Entities;
using Calculator.Domain.Interfaces;
using Calculator.Sdk.Model;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Threading.Tasks;

namespace Calculator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JournalController : ControllerBase
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly IOperationService operationService;

        public JournalController(IOperationService operationService)
        {
            this.operationService = operationService;
        }

        [HttpPost]
        public async Task<ActionResult<OperationDto>> GetById(int id)
        {
            Operation operation = await operationService.GetById(id);
            if (operation == null)
            {
                logger.Debug($"Could not find operation id {id}.");
                return NotFound();
            }

            return Ok(OperationMapper.Map(operation));
        }
    }
}
