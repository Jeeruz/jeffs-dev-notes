using JeffsDevNotes.Application.Commands;
using JeffsDevNotes.Application.Queries;
using JeffsDevNotes.Shared.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JeffsDevNotes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NoteController : ControllerBase
    {
        private readonly IMediator _mediator;

        // Injects MediatR mediator service instead of business logic services directly
        public NoteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<NoteResponse>>> GetNotes()
        {
            // Dispatches query request through MediatR pipeline to its matching Handler
            var response = await _mediator.Send(new GetNotesQuery());
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<NoteResponse>> CreateNote([FromBody] CreateNoteRequest request)
        {
            // Wraps DTO payload in a Command object and dispatches to handler
            var response = await _mediator.Send(new CreateNoteCommand(request));

            // Simple 200 OK return without location routing
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<NoteResponse>> UpdateNote([FromBody] CreateNoteRequest request)
        {
            // Wraps DTO payload in a Command object and dispatches to handler
            var response = await _mediator.Send(new UpdateNoteCommand(request));

            // Simple 200 OK return without location routing
            return Ok(response);
        }

        [HttpDelete("api/note/{id}")]
        public async Task<ActionResult<NoteResponse>> DeleteNote([FromBody] CreateNoteRequest request)
        {
            // Wraps DTO payload in a Command object and dispatches to handler
            var response = await _mediator.Send(new DeleteNoteCommand(request));

            // Simple 200 OK return without location routing
            return Ok(response);
        }
    }
}
