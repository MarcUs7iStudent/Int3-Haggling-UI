namespace HagglingContracts.Models;

public record Product (uint Id, string Name, decimal Price, ProductType Type);

public record Offer (Product Product, decimal NewPrice, decimal OldPrice);