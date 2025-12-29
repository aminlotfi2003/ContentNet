using Asp.Versioning;
using ContentNet.Application.Features.Articles.Commands.CreateArticle;
using ContentNet.Application.Features.Articles.Commands.DeleteArticle;
using ContentNet.Application.Features.Articles.Commands.PublishArticle;
using ContentNet.Application.Features.Articles.Commands.ScheduleArticle;
using ContentNet.Application.Features.Articles.Commands.UpdateArticle;
using ContentNet.Application.Features.Articles.Dtos;
using ContentNet.Application.Features.Articles.Queries.GetArticleDetails;
using ContentNet.Application.Features.Articles.Queries.GetArticles;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContentNet.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/articles")]
public class ArticleController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ArticleListItemDto>>> GetArticles(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetArticlesQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ArticleDto>> GetArticleById(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetArticleDetailsQuery(id), cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ArticleDto>> CreateArticle([FromBody] CreateArticleCommand command, CancellationToken cancellationToken)
    {
        var id = await _mediator.Send(command, cancellationToken);
        var created = await _mediator.Send(new GetArticleDetailsQuery(id), cancellationToken);
        var version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "1.0";

        return CreatedAtAction(nameof(GetArticleById), new { id, version }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateArticle(int id, [FromBody] UpdateArticleCommand command, CancellationToken cancellationToken)
    {
        var updateCommand = command with { Id = id };
        await _mediator.Send(updateCommand, cancellationToken);

        return NoContent();
    }

    [HttpPatch("{id:int}/publish")]
    public async Task<IActionResult> PublishArticle(int id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new PublishArticleCommand(id), cancellationToken);
        return NoContent();
    }

    [HttpPatch("{id:int}/schedule")]
    public async Task<IActionResult> ScheduleArticle(int id, [FromBody] DateTimeOffset scheduledAtUtc, CancellationToken cancellationToken)
    {
        await _mediator.Send(new ScheduleArticleCommand(id, scheduledAtUtc), cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteArticle(int id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteArticleCommand(id), cancellationToken);
        return NoContent();
    }
}
