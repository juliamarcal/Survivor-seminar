namespace ApiModels.Common;

public record ValidationErrorResponse(IList<ValidationErrorDetail> ValidationErrors);