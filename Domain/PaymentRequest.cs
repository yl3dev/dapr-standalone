namespace Domain;

public record PaymentRequest(string OrderId, decimal Amount);