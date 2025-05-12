using HagglingContracts.Models;
namespace HagglingContracts.Interfaces;

public interface IHuman
{
    /// <summary>
    /// Age of the person
    /// </summary>
    ushort Age { get; }
    
    /// <summary>
    /// Current mood of the person
    /// </summary>
    Mood Mood { get; }
    
    /// <summary>
    /// Name of the person
    /// </summary>
    string Name { get; }
    
    /// <summary>
    /// Gender of the person
    /// </summary>
    Gender Gender { get; }
    
    /// <summary>
    /// The marketplace this vendor does business in
    /// </summary>
    IMarketplace? CurrentMarketplace { get; }
    
    /// <summary>
    /// Things owned by this person
    /// </summary>
    List<Product> Inventory { get; }
    
    /// <summary>
    /// Money owned by this person
    /// </summary>
    decimal Funds { get; }
    
    /// <summary>
    /// Processes the offer given by a vendor based on its reasonableness and traits of the vendor,
    /// either accepting the offer, canceling the trade, or giving a counteroffer,
    /// also changing own mood accordingly 
    /// </summary>
    /// <param name="vendor"> The person offering </param>
    /// <param name="offer"> The offer to react to </param>
    /// <param name="counteroffer"> if so decided, give the other person a counteroffer, leave as null otherwise </param>
    /// <returns> decided answer of the customer </returns>
    Dialogue ProcessOffer(IHuman vendor, Offer offer, out Offer? counteroffer);
}