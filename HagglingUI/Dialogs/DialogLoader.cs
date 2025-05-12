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
    /// <param name="assembly">Used only for unit tests</param>
    /// <returns>Deserialized object or default if not found</returns>
    public static T? LoadDialogue<T>(string resourceName, Assembly? assembly = null)
    {
        assembly ??= Assembly.GetExecutingAssembly();
        var fullResourceName = $"HagglingUI.Data.{resourceName}";
        
        using var stream = assembly.GetManifestResourceStream(fullResourceName);
        if (stream == null)
        {
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
            return default;
        }
    }
    
    /// <summary>
    /// Load all dialogue files needed for the UI
    /// </summary>
    /// <param name="assembly">Used only for unit tests</param>
    /// <returns>Tuple containing all dialogue dictionaries</returns>
    public static (
        Dictionary<string, Dictionary<string, List<string>>>? CustomerDialogues,
        Dictionary<string, Dictionary<string, List<string>>>? VendorDialogues,
        Dictionary<string, Dictionary<string, List<string>>>? ErrorDialogues
    ) LoadAllDialogues(Assembly? assembly = null)
    {
        var customerDialogues = LoadDialogue<Dictionary<string, Dictionary<string, List<string>>>>("customer-dialog.json", assembly);
        var vendorDialogues = LoadDialogue<Dictionary<string, Dictionary<string, List<string>>>>("vendor-dialog.json", assembly);
        var errorDialogues = LoadDialogue<Dictionary<string, Dictionary<string, List<string>>>>("error-dialog.json", assembly);
        
        return (customerDialogues, vendorDialogues, errorDialogues);
    }
}