public enum Dialogue
{
    Greeting,
    Offer,
    Counteroffer,
    Accept,
    Decline,
    Goodbye
}

public enum Mood
{
    SunshineAndRainbows = 4,
    Happy = 3,
    Neutral = 2,
    Annoyed = 1,
    Outraged = 0
}

public enum Gender
{
    Male,
    Female,
    Other
}

public enum ProductType
{
    Food = 5,
    Clothing = 4,
    Dishware = 3,
    Tool = 2,
    Furniture = 1,
    Luxury = 0,
}

public enum Reputation
{
    Trustworthy,
    Upright,
    Sly,
    Deceitful,
    Unknown
}

public record Product (uint Id, string Name, decimal Price, ProductType Type);

public record Offer (Product Product, decimal NewPrice, decimal OldPrice);