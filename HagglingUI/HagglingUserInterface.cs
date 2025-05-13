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

    public bool PrintPersonInfo(IHuman person)
    {
        var grid = new Grid();
        grid.AddColumn();

        grid.AddRow(new Text($"Person Information", new Style(Color.Blue, Color.Grey, Decoration.Bold)).Centered());

        var infoTable = new Table()
                        .Border(TableBorder.Rounded)
                        .HideHeaders();

        infoTable.AddColumn("Property");
        infoTable.AddColumn("Value");

        infoTable.AddRow("[yellow]Name:[/]", person.Name);
        infoTable.AddRow("[yellow]Age:[/]", person.Age.ToString());
        infoTable.AddRow("[yellow]Gender:[/]", person.Gender.ToString());

        var moodColor = person.Mood switch
                        {
                            Mood.SunshineAndRainbows => Color.Green,
                            Mood.Happy               => Color.LightGreen,
                            Mood.Neutral             => Color.Yellow,
                            Mood.Annoyed             => Color.Orange1,
                            Mood.Outraged            => Color.Red,
                            _                        => Color.White
                        };

        infoTable.AddRow("[yellow]Mood:[/]", $"[{moodColor}]{person.Mood}[/]");
        infoTable.AddRow("[yellow]Funds:[/]", $"{person.Funds:C}");

        grid.AddRow(infoTable);

        // Add inventory if items exist
        if (person.Inventory.Any())
        {
            var inventoryTable = new Table()
                                 .Border(TableBorder.Rounded)
                                 .Title("[yellow]Inventory[/]");

            inventoryTable.AddColumn("Product");
            inventoryTable.AddColumn("Type");
            inventoryTable.AddColumn("Value");

            foreach (var product in person.Inventory)
            {
                inventoryTable.AddRow(
                                      product.Name,
                                      product.Type.ToString(),
                                      $"{product.Price:C}");
            }

            grid.AddRow(inventoryTable);
        }
        
        if (person.CurrentMarketplace != null)
        {
            grid.AddRow(new Markup($"[yellow]Current Marketplace:[/] {person.CurrentMarketplace}"));
        }

        AnsiConsole.Write(grid);
        return true;
    }

    public bool PrintError(DialogueError dialogueError, string errorMessage) => _screen.PrintError(dialogueError, errorMessage);

    public bool ClearScreen() => _screen.ClearScreen();
}