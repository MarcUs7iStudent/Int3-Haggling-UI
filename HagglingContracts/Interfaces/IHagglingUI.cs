using HagglingContracts.Models;

namespace HagglingContracts.Interfaces;

public interface IHagglingUI
{
    /// <summary>
    /// Prints the customer asking for a specific type of product
    /// </summary>
    /// <param name="customerAsking">Customer asking for a product</param>
    /// <param name="vendor">The Vendor</param>
    /// <param name="productType">The product customer is looking for</param>
    /// <returns>True if it could print, False if console is not attached or another error occurred.</returns>
    bool PrintInitialQuestion(IHuman customerAsking, IHuman vendor, ProductType productType);
    
    /// <summary>
    /// Prints the action the customer is taking and
    /// a line of dialogue based on the customer and if needed the offer they are making
    /// </summary>
    /// <param name="dialogue">Dialogue to select</param>
    /// <param name="personTalking">Person talking</param>
    /// <param name="partner">The partner it is currently talking to</param>
    /// <param name="offer">Offer being made</param>
    /// <returns>True if it could print, False if console is not attached or another error occurred.</returns>
    bool PrintDialogue(Dialogue dialogue, IHuman personTalking, IHuman partner, Offer? offer = null);

    /// <summary>
    /// Prints are necessary trade details
    /// </summary>
    /// <param name="customer">Customer in the trade</param>
    /// <param name="vendor">Vendor in the trade</param>
    /// <param name="product">Product being traded for</param>
    /// <returns>True if it could print, False if console is not attached or another error occurred.</returns>
    bool PrintTradeDetails(IHuman customer, IHuman vendor, Product product);

    /// <summary>
    /// Prints the final result of the trade
    /// </summary>
    /// <param name="personOffering">A duplicate of the person makeing the offer</param>
    /// <param name="customer">Customer in the trade</param>
    /// <param name="vendor">Vendor in the trade</param>
    /// <param name="finalOffer">Final offer in the trade</param>
    /// <returns>True if it could print, False if console is not attached or another error occurred.</returns>
    bool PrintTradeResult(IHuman personOffering, IHuman customer, IHuman vendor, Offer finalOffer);

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
    /// <param name="dialogueError">The type of error</param>
    /// <param name="errorMessage">The actual error message</param>
    /// <returns>True if it could print, False if console is not attached or another error occurred.</returns>
    bool PrintError(DialogueError dialogueError, string errorMessage);

    /// <summary>
    /// Clears the screen
    /// </summary>
    /// <returns>True if it could print, False if console is not attached or another error occurred.</returns>
    bool ClearScreen();

    /// <summary>
    /// Plays an embedded music
    /// Continues playing if it was previously playing or paused
    /// </summary>
    /// <returns>True if it could start playing, False if it was already playing or another error occurred.</returns>
    bool PlayAudio();

   /// <summary>
   /// Pauses the currently playing audio
   /// </summary>
   /// <returns>True if it could pause the audio, False if it's not running or another error occurred.</returns>
    bool PauseAudio();

    /// <summary>
    /// Stops the currently playing audio
    /// This will also reset the audio to the beginning
    /// </summary>
    /// <returns>True if it could stop the audio, False if the audio isn't running or another error occurred.</returns>
    bool StopAudio();
}