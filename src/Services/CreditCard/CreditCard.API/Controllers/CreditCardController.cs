using CreditCard.API.Service;
using CreditCard.API.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace CreditCard.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CreditCardController : ControllerBase
    {
        private readonly ICreditCardService _creditCardService;

        public CreditCardController(ICreditCardService creditCardService)
        {
            _creditCardService = creditCardService;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<Model.CreditCard>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            var creditCards = await _creditCardService.Get();
            return Ok(creditCards);
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Model.CreditCard), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            var creditCard = await _creditCardService.GetById(id);
            return Ok(creditCard);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult> Post([FromBody] CreditCardViewModel creditCardViewModel)
        {
            await _creditCardService.Add(creditCardViewModel);
            return Created("", creditCardViewModel);
        }

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult> Put(Guid id, [FromBody] CreditCardViewModel creditCardViewModel)
        {
            await _creditCardService.Update(id, creditCardViewModel);
            return Created("", creditCardViewModel);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _creditCardService.Delete(id);
            return NoContent();
        }
    }
}
