using NoteBook.Application.Commands;
using NoteBook.Application.Commands.Notes;
using NoteBook.Application.Queries;
using NoteBook.Shared.DTOs;
using NoteBook.Shared.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace NoteBook.Controllers
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
        public async Task<ActionResult<Result<NoteResponse>>> CreateNote([FromBody] CreateNoteRequest request)
        {
            // Wraps DTO payload in a Command object and dispatches to handler
            var response = await _mediator.Send(new CreateNoteCommand(request));

            // Simple 200 OK return without location routing
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Result<NoteResponse>>> UpdateNote(int id, [FromBody] CreateNoteRequest request)
        {
            // Wraps DTO payload in a Command object and dispatches to handler
            var response = await _mediator.Send(new UpdateNoteCommand( id, request));

            // Simple 200 OK return without location routing
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Result<NoteResponse>>> DeleteNote(int id)
        {
            // Wraps DTO payload in a Command object and dispatches to handler
            var response = await _mediator.Send(new DeleteNoteCommand(id));

            // Simple 200 OK return without location routing
            return Ok(response);
        }
    }
}
