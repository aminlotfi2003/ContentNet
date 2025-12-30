using ContentNet.Application.Common.Abstractions.Services;
using FluentValidation;
using MediatR;

namespace ContentNet.Application.Features.Auth.Commands.RequestOtp;

public class RequestOtpCommandHandler(IOtpService otpService)
    : IRequestHandler<RequestOtpCommand, Unit>
{
    private readonly IOtpService _otpService = otpService;

    public async Task<Unit> Handle(RequestOtpCommand request, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(request.PhoneNumber))
            throw new ValidationException("PhoneNumber is required.");

        await _otpService.GenerateAndSendOtpAsync(request.PhoneNumber);
        return Unit.Value;
    }
}
