namespace ApiModels.Common;

public record ValidationErrorDetail(ValidationErrorLoc Loc, string Msg, string Type);