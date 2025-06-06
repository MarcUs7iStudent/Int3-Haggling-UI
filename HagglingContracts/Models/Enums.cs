﻿namespace HagglingContracts.Models;

public enum Dialogue
{
    Greeting,
    Offer,
    Counteroffer,
    Accept,
    Decline,
    Goodbye
}

public enum DialogueError
{
    NoReasonToHaggle,
    NoItemToHaggle,
    BadCustomerName,
    BadCustomerAge,
    NoCustomerOffer,
    BadVendorName,
    BadVendorAge,
    BadVendorSellingPoint,
    BadVendorReputation,
    NoVendorOffer,
    UnexpectedError
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