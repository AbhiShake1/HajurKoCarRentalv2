namespace HajurKoCarRental.Shared.Models.RequestModels;

public record ResetPasswordRequestModel(string OldPassword, string NewPassword);