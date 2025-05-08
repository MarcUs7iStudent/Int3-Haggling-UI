using Haggling;

namespace HagglingUI;

public interface IHagglingUserInterface
{
    /// <summary>
    /// Prints the customer asking for a specific type of product
    /// </summary>
    /// <param name="customerAsking"> customer asking for a product </param>
    /// <param name="productType"> the product customer is looking for </param>
    void PrintInitialQuestion(IHuman customerAsking, ProductType productType);
    
    /// <summary>
    /// Prints the action the customer is taking and
    /// a line of dialogue based on the customer and if needed the offer they are making
    /// </summary>
    /// <param name="dialogue"> dialogue to select </param>
    /// <param name="personTalking"> person talking </param>
    /// <param name="offer"> offer being made </param>
    void PrintDialogue(Dialogue dialogue, IHuman personTalking, Offer? offer = null);

    /// <summary>
    /// Prints are necessary trade details
    /// </summary>
    /// <param name="customer"> customer in the trade </param>
    /// <param name="vendor"> vendor in the trade </param>
    /// <param name="product"> product being traded for </param>
    void PrintTradeDetails(IHuman customer, IHuman vendor, Product product);

    /// <summary>
    /// Prints the final result of the trade
    /// </summary>
    /// <param name="customer"> customer in the trade </param>
    /// <param name="vendor"> vendor in the trade </param>
    /// <param name="finalOffer"> final offer in the trade </param>
    void PrintTradeResult(IHuman customer, IHuman vendor, Offer finalOffer);

    /// <summary>
    /// Prints the introduction of a person e.g. "John, a local peasant"
    /// </summary>
    /// <param name="person"></param>
    void PrintPersonIntroduction(IHuman person);

    /// <summary>
    /// Prints all useful info about a person
    /// </summary>
    /// <param name="person"></param>
    void PrintPersonInfo(IHuman person);
}