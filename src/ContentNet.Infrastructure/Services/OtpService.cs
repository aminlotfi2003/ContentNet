using ContentNet.Application.Common.Abstractions.Services;
using ContentNet.Domain.Entities;
using ContentNet.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ContentNet.Infrastructure.Services;

public class OtpService : IOtpService
{
    private readonly ApplicationDbContext _db;
    private readonly ISmsSenderService _sms;

    public OtpService(ApplicationDbContext db, ISmsSenderService sms)
    {
        _db = db;
        _sms = sms;
    }

    public async Task GenerateAndSendOtpAsync(string phoneNumber)
    {
        var code = new Random().Next(100000, 999999).ToString(); // 6-digit code
        var otp = new OtpCode
        {
            Code = code,
            PhoneNumber = phoneNumber,
            ExpiresAt = DateTimeOffset.UtcNow.AddMinutes(3),
            IsUsed = false
        };
        await _db.OtpCodes.AddAsync(otp);
        await _db.SaveChangesAsync();
        await _sms.SendSmsAsync(phoneNumber, $"Your OTP code is: {code}");
    }

    public async Task<bool> ValidateOtpAsync(string phoneNumber, string code)
    {
        var otp = await _db.OtpCodes
            .Where(x => x.PhoneNumber == phoneNumber && x.Code == code && !x.IsUsed && x.ExpiresAt > DateTimeOffset.UtcNow)
            .OrderByDescending(x => x.ExpiresAt)
            .FirstOrDefaultAsync();

        if (otp == null) return false;
        otp.IsUsed = true;
        await _db.SaveChangesAsync();
        return true;
    }
}
