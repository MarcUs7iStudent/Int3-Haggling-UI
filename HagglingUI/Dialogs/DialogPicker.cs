namespace HagglingUI.Dialogs;

public class DialogPicker
{
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
        
        if (person.Inventory.Count != 0)
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
    
    public bool PrintInitialQuestion(IHuman customerAsking, ProductType productType)
    {
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
}
