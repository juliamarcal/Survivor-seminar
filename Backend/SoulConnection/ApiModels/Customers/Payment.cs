namespace ApiModels.Customers;

public record Payment(int Id, string Date, string PaymentMethod, decimal Amount, string Comment);