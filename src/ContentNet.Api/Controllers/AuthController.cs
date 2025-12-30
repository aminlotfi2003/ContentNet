using Asp.Versioning;
using ContentNet.Application.Features.Auth.Commands.RequestOtp;
using ContentNet.Application.Features.Auth.Commands.VerifyOtp;
using ContentNet.Application.Features.Auth.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContentNet.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/auth")]
public class AuthController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("request")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public new async Task<IActionResult> Request([FromBody] RequestOtpCommand command, CancellationToken ct)
    {
        await _mediator.Send(command, ct);
        return NoContent();
    }

    [HttpPost("verify")]
    [ProducesResponseType(typeof(VerifyOtpResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<VerifyOtpResultDto>> Verify([FromBody] VerifyOtpCommand command, CancellationToken ct)
        => Ok(await _mediator.Send(command, ct));
}
