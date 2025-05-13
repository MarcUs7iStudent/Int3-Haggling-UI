using HagglingUI.Audio;
using HagglingUI.Screen;
using HagglingContracts.Models;
using HagglingContracts.Interfaces;
using Spectre.Console;

namespace HagglingUI;

public class HagglingUserInterface : IHagglingUI
{
    private readonly IScreen _screen = new Screen.Screen();
    private readonly IAudio _audio = new Audio.Audio();

    public bool PrintInitialQuestion(IHuman customerAsking, IHuman vendor, ProductType productType)
    {
        string question = productType switch
    {
        ProductType.Food => "[green]food items[/]",
        ProductType.Clothing => "[blue]clothing[/]",
        ProductType.Dishware => "[yellow]dishware[/]",
        ProductType.Tool => "[orange]tools[/]",
        ProductType.Furniture => "[purple]furniture[/]",
        ProductType.Luxury => "[gold]luxury goods[/]",
        _ => "[grey]items[/]"
    };
        
    var grid = new Grid()
        .AddColumns(2)
        .AddRow(
            new Panel(
                new Rows(
                    new Markup($"[bold yellow]Customer:[/] {customerAsking.Name}"),
                    new Markup($"[grey]Mood:{customerAsking.Mood}[/]"),
                    new Markup($"[grey]Funds: {customerAsking.Funds:C}[/]")
                ))
            {
                Border = BoxBorder.Rounded,
                Padding = new Padding(1)
            },
            new Panel(
                new Rows(
                    new Markup($"[bold cyan]Vendor:[/] {vendor.Name}"),
                    new Markup($"[grey]Mood:{vendor.Mood}[/]"),
                    new Markup($"[grey]Market: {vendor.CurrentMarketplace!.ToString() ?? "None"}[/]")
                ))
            {
                Border = BoxBorder.Rounded,
                Padding = new Padding(1)
            }
        );
    var dialogPanel = new Panel(
        new Rows(
            new Markup($"[bold]{customerAsking.Name}[/] asks [bold]{vendor.Name}[/]:"),
            new Markup($"[white]\"Do you have any {question} available?\"[/]"),
            new Text("")
        ))
    {
        Border = BoxBorder.Double,
        Header = new PanelHeader(" Initial Question ", Justify.Center),
        Padding = new Padding(2, 1, 2, 1)
    };
    
    var layout = new Layout("Root")
        .SplitRows(
            new Layout("Dialog", dialogPanel),
            new Layout("Participants", grid)
        );

        AnsiConsole.Write(layout);
        return true;
    }

    public bool PrintDialogue(Dialogue dialogue, IHuman personTalking, IHuman partner, Offer? offer = null) => _screen.PrintDialogue(dialogue, personTalking, partner, offer);

    public bool PrintTradeDetails(IHuman customer, IHuman vendor, Product product) => _screen.PrintTradeDetails(customer, vendor, product);
    
    /// <summary>
    /// Print the result (final price, etc.)
    /// </summary>
    /// <param name="personOffering"></param>
    /// <param name="customer"></param>
    /// <param name="vendor"></param>
    /// <param name="finalOffer"></param>
    /// <returns>False if no Console is attached, else print the result.</returns>
    public bool PrintTradeResult(IHuman personOffering, IHuman customer, IHuman vendor, Offer finalOffer)
    {
        if (!ConsoleCheck.IsConsoleAttached())
        {
            return false;
        }
        var tradeSummaryTable = new Table()
                                .Border(TableBorder.Rounded)
                                .Title("[bold]Trade Summary[/]")
                                .AddColumn("")
                                .AddColumn("");

        tradeSummaryTable.AddRow("[green]✓ Deal Completed![/]", "");
        tradeSummaryTable.AddRow("",
                                 $"[grey]{finalOffer.Product.Name} bought for [bold]{finalOffer.NewPrice} coins[/].[/]");
        tradeSummaryTable.AddRow("",
                                 $"Customer is [green]{customer.Mood}[/], Vendor is [yellow]{vendor.Mood}[/]");

        AnsiConsole.Write(tradeSummaryTable);

        return true;
    }

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

    public bool PlayAudio() => _audio.PlayAudio();

    public bool PauseAudio() => _audio.PauseAudio();
    
    public bool StopAudio() => _audio.StopAudio();
}