using Calculator.Api.Infrastructure;
using Calculator.Api.Mappers;
using Calculator.Domain.Entities;
using Calculator.Domain.Interfaces;
using Calculator.Sdk.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Calculator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly IOperationService operationService;

        public CalculatorController(IOperationService operationService)
        {
            this.operationService = operationService;
        }

        /// <summary>
        /// Compute the addition of two or more operands.
        /// </summary>
        /// <param name="sumDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Sum")]
        [IdHeaderParserFilter]
        public async Task<ActionResult<OperationDto>> Sum(AditionDto sumDto)
        {
            if (sumDto.Addends.Count < 2)
                return BadRequest(new { Message = "At least 2 parameters must be specified for this operation." });
            try
            {
                Operation operation = await operationService.Add(sumDto.OperationId, sumDto.Addends);
                return Ok(OperationMapper.Map(operation));
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                return Problem("There was a problem processing your request", statusCode: (int?)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Compute the subtraction of two operands
        /// </summary>
        /// <param name="subDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Sub")]
        [IdHeaderParserFilter]
        public async Task<ActionResult<OperationDto>> Sub(SubtractionDto subDto)
        {
            try
            {
                Operation operation = await operationService.Sub(subDto.OperationId, subDto.Minuend, subDto.Subtrahend);
                return Ok(OperationMapper.Map(operation));
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                return Problem("There was a problem processing your request", statusCode: (int?)HttpStatusCode.InternalServerError);
            }
        }


        /// <summary>
        /// Compute the multiplication of two operands
        /// </summary>
        /// <param name="subDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Mult")]
        [IdHeaderParserFilter]
        public async Task<ActionResult<OperationDto>> Mult(MultiplyDto multiplyDto)
        {
            if (multiplyDto.Factors.Count < 2)
                return BadRequest(new { Message = "At least 2 parameters must be specified for this operation." });
            try
            {
                Operation operation = await operationService.Mult(multiplyDto.OperationId, multiplyDto.Factors);
                return Ok(OperationMapper.Map(operation));
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                return Problem("There was a problem processing your request", statusCode: (int?)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Compute the division of two operands
        /// </summary>
        /// <param name="subDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Div")]
        [IdHeaderParserFilter]
        public async Task<ActionResult<OperationDto>> Div(DivisionDto divisionDto)
        {
            if (divisionDto.Divisor == 0)
                return BadRequest(new { Message = "Can not divide by 0." });
            try
            {
                Operation operation = await operationService.Div(divisionDto.OperationId, divisionDto.Dividend, divisionDto.Divisor);
                return Ok(OperationMapper.Map(operation));
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                return Problem("There was a problem processing your request", statusCode: (int?)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Compute the square root.
        /// </summary>
        /// <param name="subDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Sqrt")]
        [IdHeaderParserFilter]
        public async Task<ActionResult<OperationDto>> Sqrt(SqrtDto sqrtDto)
        {
            try
            {
                Operation operation = await operationService.Sqrt(sqrtDto.OperationId, sqrtDto.Operand);
                return Ok(OperationMapper.Map(operation));
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                return Problem("There was a problem processing your request", statusCode: (int?)HttpStatusCode.InternalServerError);
            }
        }

    }
}
