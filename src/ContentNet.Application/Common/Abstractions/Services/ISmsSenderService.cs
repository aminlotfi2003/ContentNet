namespace ContentNet.Application.Common.Abstractions.Services;

public interface ISmsSenderService
{
    Task SendSmsAsync(string phoneNumber, string message);
}
