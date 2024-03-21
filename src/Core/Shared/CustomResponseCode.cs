namespace Jpl.MicroService.Shared;
public enum CustomResponseCode
{
    DEFAULT = 0,
    EmailPasswordNotCorrect = 401001,
    MaximumAttemptLogin = 401002,
    WaitingToLogin = 401003,
    SessionExpired = 401004,
    InvalidRefreshToken = 401005,
    ResetPasswordTokenExpired = 401006,
    EmailNotExist = 404001,
    ItemExisted=500001
}
