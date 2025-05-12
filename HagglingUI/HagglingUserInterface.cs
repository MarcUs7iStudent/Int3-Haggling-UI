using Haggling;
using HagglingUI.Audio;
using HagglingUI.Dialogs;
using HagglingUI.Screen;

namespace HagglingUI;

public class HagglingUserInterface : IHagglingUserInterface
{
    private readonly IScreen _screen = new Screen.Screen();
    private readonly IAudio _audio = new Audio.Audio();

    public bool PrintInitialQuestion(IHuman customerAsking, ProductType productType) => _screen.PrintInitialQuestion(customerAsking, productType);

    public bool PrintDialogue(Dialogue dialogue, IHuman personTalking, Offer? offer = null) => _screen.PrintDialogue(dialogue, personTalking, offer);

    public bool PrintTradeDetails(IHuman customer, IHuman vendor, Product product) => _screen.PrintTradeDetails(customer, vendor, product);

    public bool PrintTradeResult(IHuman customer, IHuman vendor, Offer finalOffer) => _screen.PrintTradeResult(customer, vendor, finalOffer);

    public bool PrintPersonIntroduction(IHuman person) => _screen.PrintPersonIntroduction(person);

    public bool PrintPersonInfo(IHuman person) => _screen.PrintPersonInfo(person);

    public bool PrintError(string errorMessage) => _screen.PrintError(errorMessage);

    public bool ClearScreen() => _screen.ClearScreen();

    public bool PlayAudio() => _audio.PlayAudio();

    public bool PauseAudio() => _audio.PauseAudio();
    
    public bool StopAudio() => _audio.StopAudio();
}