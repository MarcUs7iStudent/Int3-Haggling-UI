using HagglingContracts.Interfaces;
using HagglingContracts.Models;

namespace HagglingUI.Screen;

public interface IScreen
{
    /// <summary>
    /// Prints the customer asking for a specific type of product
    /// </summary>
    /// <param name="customerAsking"> customer asking for a product </param>
    /// <param name="productType"> the product customer is looking for </param>
    /// <returns>True if it could print, False if console is not attached or another error occurred.</returns>
    bool PrintInitialQuestion(IHuman customerAsking, ProductType productType);
    
    /// <summary>
    /// Prints the action the customer is taking and
    /// a line of dialogue based on the customer and if needed the offer they are making
    /// </summary>
    /// <param name="dialogue"> dialogue to select </param>
    /// <param name="personTalking"> person talking </param>
    /// <param name="offer"> offer being made </param>
    /// <returns>True if it could print, False if console is not attached or another error occurred.</returns>
    bool PrintDialogue(Dialogue dialogue, IHuman personTalking, Offer? offer = null);

    /// <summary>
    /// Prints are necessary trade details
    /// </summary>
    /// <param name="customer"> customer in the trade </param>
    /// <param name="vendor"> vendor in the trade </param>
    /// <param name="product"> product being traded for </param>
    /// <returns>True if it could print, False if console is not attached or another error occurred.</returns>
    bool PrintTradeDetails(IHuman customer, IHuman vendor, Product product);

    /// <summary>
    /// Prints the final result of the trade
    /// </summary>
    /// <param name="customer"> customer in the trade </param>
    /// <param name="vendor"> vendor in the trade </param>
    /// <param name="finalOffer"> final offer in the trade </param>
    /// <returns>True if it could print, False if console is not attached or another error occurred.</returns>
    bool PrintTradeResult(IHuman customer, IHuman vendor, Offer finalOffer);

    /// <summary>
    /// Prints the introduction of a person e.g. "John, a local peasant"
    /// </summary>
    /// <param name="person"></param>
    /// <returns>True if it could print, False if console is not attached or another error occurred.</returns>
    bool PrintPersonIntroduction(IHuman person);

    /// <summary>
    /// Prints all useful info about a person
    /// </summary>
    /// <param name="person"></param>
    /// <returns>True if it could print, False if console is not attached or another error occurred.</returns>
    bool PrintPersonInfo(IHuman person);

    /// <summary>
    /// Clears the screen, prints a funny error message and afterward the actual error message
    /// </summary>
    /// <param name="errorMessage">The actual error message</param>
    /// <returns>True if it could print, False if console is not attached or another error occurred.</returns>
    bool PrintError(DialogueError dialogueError, string errorMessage);

    /// <summary>
    /// Clears the screen
    /// </summary>
    /// <returns>True if it could print, False if console is not attached or another error occurred.</returns>
    bool ClearScreen();
}