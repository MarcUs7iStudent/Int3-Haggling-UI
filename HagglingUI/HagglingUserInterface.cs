using Haggling;

namespace HagglingUI;

public class HagglingUserInterface : IHagglingUserInterface
{
    public void PrintInitialQuestion(IHuman customerAsking, ProductType productType)
    {
        Console.WriteLine("PrintInitialQuestion called");
    }

    public void PrintDialogue(Dialogue dialogue, IHuman personTalking, Offer? offer = null)
    {
        Console.WriteLine("PrintDialogue called");
    }

    public void PrintTradeDetails(IHuman customer, IHuman vendor, Product product)
    {
        Console.WriteLine("PrintTradeDetails called");
    }

    public void PrintTradeResult(IHuman customer, IHuman vendor, Offer finalOffer)
    {
        Console.WriteLine("PrintTradeResult called");
    }

    public void PrintPersonIntroduction(IHuman person)
    {
        Console.WriteLine("PrintPersonIntroduction called");
    }

    public void PrintPersonInfo(IHuman person)
    {
        Console.WriteLine("PrintPersonInfo called");
    }
}