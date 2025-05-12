using HagglingContracts.Interfaces;
using HagglingContracts.Models;

namespace HagglingUI.Screen;

public class Screen : IScreen
{
    public bool PrintInitialQuestion(IHuman customerAsking, ProductType productType)
    {
        Console.WriteLine("PrintInitialQuestion called");
        return false;
    }

    public bool PrintDialogue(Dialogue dialogue, IHuman personTalking, Offer? offer = null)
    {
        Console.WriteLine("PrintDialogue called");
        return false;
    }

    public bool PrintTradeDetails(IHuman customer, IHuman vendor, Product product)
    {
        Console.WriteLine("PrintTradeDetails called");
        return false;
    }

    public bool PrintTradeResult(IHuman customer, IHuman vendor, Offer finalOffer)
    {
        Console.WriteLine("PrintTradeResult called");
        return false;
    }

    public bool PrintPersonIntroduction(IHuman person)
    {
        Console.WriteLine("PrintPersonIntroduction called");
        return false;
    }

    public bool PrintPersonInfo(IHuman person)
    {
        Console.WriteLine("PrintPersonInfo called");
        return false;
    }

    public bool PrintError(string errorMessage)
    {
        Console.WriteLine("PrintError called");
        return false;
    }

    public bool ClearScreen()
    {
        Console.WriteLine("ClearScreen called");
        return false;
    }
}