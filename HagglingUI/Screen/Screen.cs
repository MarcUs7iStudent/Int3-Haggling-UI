using HagglingContracts.Interfaces;
using HagglingContracts.Models;
using Spectre.Console;
namespace HagglingUI.Screen;

public class Screen : IScreen
{
    public bool PrintInitialQuestion(IHuman customerAsking, IHuman vendor, ProductType productType)
    {
        //TODO: Should use Dialogs
        string question = productType switch 
        {

            ProductType.Food      => $"Do you have any food for sale?",
            ProductType.Clothing  => $"Do you have any clothing available?",
            ProductType.Dishware  => $"Do you sell dishware?",
            ProductType.Tool      => $"Do you have any tools for sale?",
            ProductType.Furniture => $"Do you sell furniture?",
            ProductType.Luxury    => $"Do you have any luxury items?",
            _                     => $"Do you have anything for sale?"
        };


        AnsiConsole.MarkupLine($"[yellow]{customerAsking.Name}[/]: {question}");


        return true;
    }

public bool PrintDialogue(Dialogue dialogue, IHuman personTalking, IHuman partner, Offer? offer = null)
{
    if (!ConsoleCheck.IsConsoleAttached())
    {
        return false;
    }

    var dialogPicker = new HagglingUI.Dialogs.DialogPicker();
    var isCustomer = personTalking is ICustomer;
    
    string selected = isCustomer 
        ? dialogPicker.GetCustomerDialogue(dialogue, personTalking.Mood)
        : dialogPicker.GetVendorDialogue(dialogue, personTalking.Mood);
    
    if (string.IsNullOrEmpty(selected))
    {
        return false;
    }

    var formattedDialog = offer != null
        ? Dialogs.DialogHelper.FormatDialog(selected, isCustomer ? partner : personTalking, 
            isCustomer ? personTalking : partner, 
            offer.Product, offer.NewPrice, offer.OldPrice)
        : selected; 

    AnsiConsole.MarkupLine($"[cyan]{personTalking.Name}[/]: {formattedDialog}");
    return true;
}

    public bool PrintTradeDetails(IHuman customer, IHuman vendor, Product product)
    {
        Console.WriteLine("PrintTradeDetails called");
        return false;
    }

    public bool PrintTradeResult(IHuman personOffering, IHuman customer, IHuman vendor, Offer finalOffer)
    {
        Console.WriteLine("PrintTradeResult called");
        return false;
    }

    public bool PrintPersonIntroduction(IHuman person)
    {
        if (!ConsoleCheck.IsConsoleAttached())
        {
            return false;
        }

        string roleDesc = person is IVendor ? "[green]Vendor[/]" : 
                         (person is ICustomer ? "[blue]Customer[/]" : "");
        
        AnsiConsole.Write(new Panel($"{roleDesc}: [bold]{person.Name}[/], {person.Gender}, age {person.Age}")
            .Border(BoxBorder.Rounded)
            .BorderColor(Color.Yellow));
        return true;
    }

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
    
        // Add marketplace if available
        if (person.CurrentMarketplace != null)
        {
            grid.AddRow(new Markup($"[yellow]Current Marketplace:[/] {person.CurrentMarketplace}"));
        }
    
        AnsiConsole.Write(grid);
        return true;
    }

    public bool PrintError(DialogueError dialogueError, string errorMessage)
    {
        Console.WriteLine("PrintError called");
        return false;
    }

    public bool ClearScreen()
    {
        if (!ConsoleCheck.IsConsoleAttached())
        {
            return false;
        }
        
        AnsiConsole.Clear();
        return true;
    }
}