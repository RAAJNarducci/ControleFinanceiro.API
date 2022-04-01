using BankTransactions.API.Commands;
using BankTransactions.API.Model;
using Hangfire;
using Logs.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BankTransactions.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator _mediator;


        public TransactionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<Transaction>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            var jobId = BackgroundJob.Schedule(
                () => TestHangFire(),
                TimeSpan.FromSeconds(20));

            var command = new FindAllTransactionCommand();
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var command = new FindTransactionCommand()
            {
                Id = id
            };

            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] NewTransactionCommand newTransactionCommand)
        {
            if (!ModelState.IsValid)
            {
                BadRequest();
            }

            var response = await _mediator.Send(newTransactionCommand);

            return Ok(response);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [NonAction]
        public void TestHangFire()
        {
            Console.WriteLine("Test Hangfire");
        }
    }
}
