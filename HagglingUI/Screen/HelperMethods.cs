using Spectre.Console;
namespace HagglingUI.Screen;

public static class HelperMethods
{
    /// <summary>
    /// Helper method to print character dialogue in a styled format.
    /// </summary>
    static void ShowDialogue(string role, string name, string price, string line)
    {
        var color = role == "Vendor" ? "orange1" : "blue";
        AnsiConsole.MarkupLine($"[bold {color}]{name} ({role}):[/] {line} ([grey]{price} coins[/])");
    }
}
