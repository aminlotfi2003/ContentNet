using ContentNet.Application.Features.Auth.Dtos;
using MediatR;

namespace ContentNet.Application.Features.Auth.Commands.VerifyOtp;

public record VerifyOtpCommand(string PhoneNumber, string Code) : IRequest<VerifyOtpResultDto>;
