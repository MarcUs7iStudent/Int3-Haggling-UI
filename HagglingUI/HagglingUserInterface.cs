using HagglingUI.Screen;
using HagglingContracts.Models;
using HagglingContracts.Interfaces;
using Spectre.Console;

namespace HagglingUI;

public class HagglingUserInterface : IHagglingUI
{
    private readonly IScreen _screen = new Screen.Screen();

    public bool PrintInitialQuestion(IHuman customerAsking, IHuman vendor, ProductType productType) => _screen.PrintInitialQuestion(customerAsking, vendor, productType);

    public bool PrintDialogue(Dialogue dialogue, IHuman personTalking, IHuman partner, Offer? offer = null) => _screen.PrintDialogue(dialogue, personTalking, partner, offer);

    public bool PrintTradeDetails(IHuman customer, IHuman vendor, Product product) => _screen.PrintTradeDetails(customer, vendor, product);

    public bool PrintTradeResult(IHuman personOffering, IHuman customer, IHuman vendor, Offer finalOffer) => _screen.PrintTradeResult(personOffering, customer, vendor, finalOffer);

    public bool PrintPersonIntroduction(IHuman person) => _screen.PrintPersonIntroduction(person);

    public bool PrintPersonInfo(IHuman person) => _screen.PrintPersonInfo(person);

    public bool PrintError(DialogueError dialogueError, string errorMessage) => _screen.PrintError(dialogueError, errorMessage);

    public bool ClearScreen() => _screen.ClearScreen();
}