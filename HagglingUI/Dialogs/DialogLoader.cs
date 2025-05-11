using System.Reflection;
using System.Text.Json;

namespace HagglingUI.Dialogs;

/// <summary>
/// Utility class for loading embedded dialogue resources
/// </summary>
public static class DialogLoader
{
    /// <summary>
    /// Load a dialogue file from embedded resources
    /// </summary>
    /// <typeparam name="T">Type to deserialize to</typeparam>
    /// <param name="resourceName">Name of the JSON file</param>
    /// <returns>Deserialized object or default if not found</returns>
    public static T? LoadDialogue<T>(string resourceName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var fullResourceName = $"HagglingUI.Data.{resourceName}";
        
        using var stream = assembly.GetManifestResourceStream(fullResourceName);
        if (stream == null)
        {
            Console.Error.WriteLine($"Error: Could not find embedded resource '{fullResourceName}'");
            return default;
        }
        
        using var reader = new StreamReader(stream);
        var jsonContent = reader.ReadToEnd();
        
        try
        {
            return JsonSerializer.Deserialize<T>(jsonContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
        catch (JsonException ex)
        {
            Console.Error.WriteLine($"Error deserializing '{resourceName}': {ex.Message}");
            return default;
        }
    }
    
    /// <summary>
    /// Load all dialogue files needed for the UI
    /// </summary>
    /// <returns>Tuple containing all dialogue dictionaries</returns>
    public static (
        Dictionary<string, Dictionary<string, List<string>>>? CustomerDialogues,
        Dictionary<string, Dictionary<string, List<string>>>? VendorDialogues,
        Dictionary<string, Dictionary<string, List<string>>>? ErrorDialogues
    ) LoadAllDialogues()
    {
        var customerDialogues = LoadDialogue<Dictionary<string, Dictionary<string, List<string>>>>("customer-dialogue.json");
        var vendorDialogues = LoadDialogue<Dictionary<string, Dictionary<string, List<string>>>>("vendor-dialogue.json");
        var errorDialogues = LoadDialogue<Dictionary<string, Dictionary<string, List<string>>>>("error-dialogue.json");
        
        return (customerDialogues, vendorDialogues, errorDialogues);
    }
}