namespace ApiModels.Customers.Responses;

public record GetPaymentsHistoryResponse(IList<Payment> Payments);