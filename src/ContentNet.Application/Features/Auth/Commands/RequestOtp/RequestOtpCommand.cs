using MediatR;

namespace ContentNet.Application.Features.Auth.Commands.RequestOtp;

public record RequestOtpCommand(string PhoneNumber) : IRequest<Unit>;
