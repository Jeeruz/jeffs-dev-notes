using JeffsDevNotes.Application.Commands;
using JeffsDevNotes.Application.Queries;
using JeffsDevNotes.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using JeffsDevCategorys.Application.Commands;

namespace JeffsDevNotes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        // Injects MediatR mediator service instead of business logic services directly
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryResponse>>> GetCategories()
        {
            // Dispatches query request through MediatR pipeline to its matching Handler
            var response = await _mediator.Send(new GetCategoryQuery());
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryResponse>> CreateCategory([FromBody] CreateCategoryRequest request)
        {
            // Wraps DTO payload in a Command object and dispatches to handler
            var response = await _mediator.Send(new CreateCategoryCommand(request));

            // Simple 200 OK return without location routing
            return Ok(response);
        }
    }
}
