using ContentNet.Application.Common.Abstractions.Services;
using ContentNet.Application.Features.Auth.Dtos;
using ContentNet.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ContentNet.Application.Features.Auth.Commands.VerifyOtp;

public class VerifyOtpCommandHandler(
    UserManager<User> userManager,
    IOtpService otpService,
    IJwtTokenService jwtTokenService)
    : IRequestHandler<VerifyOtpCommand, VerifyOtpResultDto>
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly IOtpService _otpService = otpService;
    private readonly IJwtTokenService _jwtTokenService = jwtTokenService;

    public async Task<VerifyOtpResultDto> Handle(VerifyOtpCommand request, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(request.PhoneNumber))
            throw new ValidationException("PhoneNumber is required.");

        if (string.IsNullOrWhiteSpace(request.Code))
            throw new ValidationException("Code is required.");

        var valid = await _otpService.ValidateOtpAsync(request.PhoneNumber, request.Code);
        if (!valid)
            throw new ValidationException("Invalid or expired OTP.");

        var user = await _userManager.Users
            .SingleOrDefaultAsync(u => u.PhoneNumber == request.PhoneNumber, ct);

        if (user is null)
        {
            user = new User
            {
                UserName = request.PhoneNumber,
                PhoneNumber = request.PhoneNumber,
                PhoneNumberConfirmed = true,
                LastLoginAt = DateTimeOffset.UtcNow
            };

            var createResult = await _userManager.CreateAsync(user);
            if (!createResult.Succeeded)
                throw new ValidationException(string.Join(" | ", createResult.Errors.Select(e => e.Description)));
        }
        else
        {
            user.LastLoginAt = DateTimeOffset.UtcNow;

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
                throw new ValidationException(string.Join(" | ", updateResult.Errors.Select(e => e.Description)));
        }

        var token = _jwtTokenService.GenerateToken(user);
        return new VerifyOtpResultDto(token);
    }
}
