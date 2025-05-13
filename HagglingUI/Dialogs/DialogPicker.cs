using HagglingContracts.Models;

namespace HagglingUI.Dialogs;

public class DialogPicker
{
    private readonly Dictionary<string, Dictionary<string, List<string>>> _customerDialogues;
    private readonly Dictionary<string, Dictionary<string, List<string>>> _vendorDialogues;
    private readonly Dictionary<string, Dictionary<string, List<string>>> _errorDialogues;
    private readonly Random _random = new();

    public DialogPicker()
    {
        var (customer, vendor, error) = DialogLoader.LoadAllDialogues();
        if (customer == null || vendor == null || error == null)
        {
            _customerDialogues = new Dictionary<string, Dictionary<string, List<string>>>();
            _vendorDialogues = new Dictionary<string, Dictionary<string, List<string>>>();
            _errorDialogues = new Dictionary<string, Dictionary<string, List<string>>>();
            return;
        }
        _customerDialogues = customer;
        _vendorDialogues = vendor;
        _errorDialogues = error;
    }

    // Constructor for unit testing
    internal DialogPicker(
        Dictionary<string, Dictionary<string, List<string>>> customerDialogues,
        Dictionary<string, Dictionary<string, List<string>>> vendorDialogues,
        Dictionary<string, Dictionary<string, List<string>>> errorDialogues)
    {
        _customerDialogues = customerDialogues;
        _vendorDialogues = vendorDialogues;
        _errorDialogues = errorDialogues;
    }

    public string GetCustomerDialogue(Dialogue dialogueType, Mood mood)
    {
        return GetDialogue(_customerDialogues, dialogueType, mood);
    }

    public string GetVendorDialogue(Dialogue dialogueType, Mood mood)
    {
        return GetDialogue(_vendorDialogues, dialogueType, mood);
    }

    public string GetErrorMessage(DialogueError dialogueError = DialogueError.UnexpectedError, Mood mood = Mood.Neutral)
    {
        if (!_errorDialogues.TryGetValue("Error", out var errorCategory))
        {
            return string.Empty;
        }

        if (!errorCategory.ContainsKey(dialogueError.ToString()))
        {
            return string.Empty;
        }

        var options = errorCategory[dialogueError.ToString()];
        return PickRandom(options);
    }

    private string GetDialogue(Dictionary<string, Dictionary<string, List<string>>> dialogues, Dialogue dialogueType,
        Mood mood = Mood.Neutral)
    {
        if (!dialogues.ContainsKey(dialogueType.ToString()))
        {
            return string.Empty;
        }

        var dialogueOptions = dialogues[dialogueType.ToString()];
    
        if (dialogueOptions.ContainsKey(mood.ToString()))
        {
            var options = dialogueOptions[mood.ToString()];
            return PickRandom(options);
        }
        
        if (mood != Mood.Neutral && dialogueOptions.ContainsKey(Mood.Neutral.ToString()))
        {
            var options = dialogueOptions[Mood.Neutral.ToString()];
            return PickRandom(options);
        }
    
        return string.Empty;
    }

    private string PickRandom(List<string> options)
    {
        if (options.Count == 0)
        {
            return string.Empty;
        }
            
        int index = _random.Next(options.Count);
        return options[index];
    }
}