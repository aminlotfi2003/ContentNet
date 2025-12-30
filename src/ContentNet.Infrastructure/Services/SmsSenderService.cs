using ContentNet.Application.Common.Abstractions.Services;

namespace ContentNet.Infrastructure.Services;

public class SmsSenderService : ISmsSenderService
{
    public Task SendSmsAsync(string phoneNumber, string message)
    {
        Console.WriteLine($"SMS to {phoneNumber}: {message}");
        return Task.CompletedTask;
    }
}
