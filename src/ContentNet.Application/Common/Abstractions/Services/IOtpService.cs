namespace ContentNet.Application.Common.Abstractions.Services;

public interface IOtpService
{
    Task GenerateAndSendOtpAsync(string phoneNumber);
    Task<bool> ValidateOtpAsync(string phoneNumber, string code);
}
