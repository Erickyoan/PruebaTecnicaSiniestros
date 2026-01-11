using Application.Siniestros.Commands;
using Application.Siniestros.Querys;

using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SiniestrosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SiniestrosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSiniestroCommand command)
        {
            
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(Get), new { id }, id);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetSiniestrosQuery query)
        {
            
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}