using System.Text.Json;
using HagglingContracts.Models;
using HagglingUI.Dialogs;
using Xunit;

namespace HagglingUI.Test.Dialogs;

public class DialogPickerTest
{
    private readonly DialogPicker _dialogPicker = new();

    [Fact]
    public void GetCustomerDialogue_WithValidInput_ReturnsNonEmptyString()
    {
        var dialogueType = Dialogue.Greeting;
        var mood = Mood.Neutral;
        
        var result = _dialogPicker.GetCustomerDialogue(dialogueType, mood);
        
        Assert.False(string.IsNullOrWhiteSpace(result));
    }
    
    [Fact]
    public void GetVendorDialogue_WithValidInput_ReturnsNonEmptyString()
    {
        var dialogueType = Dialogue.Greeting;
        var mood = Mood.Neutral;
        
        var result = _dialogPicker.GetVendorDialogue(dialogueType, mood);
        
        Assert.False(string.IsNullOrWhiteSpace(result));
    }
    
    [Fact]
    public void GetErrorMessage_WithValidInput_ReturnsNonEmptyString()
    {
        var dialogueError = DialogueError.UnexpectedError;
        var mood = Mood.Neutral;
        
        var result = _dialogPicker.GetErrorMessage(dialogueError, mood);
        
        Assert.False(string.IsNullOrWhiteSpace(result));
    }
    
    [Fact]
    public void DialogPickerConstructor_LoadsAllDialogues()
    {
        // Load the error dialogue separately to verify it loaded correctly
        var errorDialogue = DialogLoader.LoadDialogue<Dictionary<string, Dictionary<string, List<string>>>>("error-dialog.json");
        
        // Assert the error dialogue contains the Error key and has content
        Assert.NotNull(errorDialogue);
        Assert.True(errorDialogue.ContainsKey("Error"));
        Assert.True(errorDialogue["Error"].Count > 0);
        Assert.Contains(DialogueError.UnexpectedError.ToString(), errorDialogue["Error"].Keys);
    }
    
    [Fact]
    public void GetErrorMessage_FallsBackToDefault_WhenNoDialogueFound()
    {
        // Test with invalid dialog error
        var result = _dialogPicker.GetErrorMessage((DialogueError)999);
        
        Assert.Equal(string.Empty, result);
    }
    
    [Theory]
    [InlineData(Mood.Happy)]
    [InlineData(Mood.Annoyed)]
    [InlineData(Mood.Outraged)]
    public void GetDialogue_ReturnsValidMessage_ForDifferentMoods(Mood mood)
    {
        // Verify that different moods work with customer dialogues
        var result = _dialogPicker.GetCustomerDialogue(Dialogue.Greeting, mood);
        
        // The result should be non-empty (either the specific mood text or fallback to neutral)
        Assert.False(string.IsNullOrWhiteSpace(result));
    }
    
    [Fact]
    public void RandomSelection_ReturnsVariableResults()
    {
        var results = new HashSet<string>();
        for (int i = 0; i < 50; i++)
        {
            var result = _dialogPicker.GetErrorMessage(DialogueError.UnexpectedError);
            if (!string.IsNullOrEmpty(result))
            {
                results.Add(result);
            }
        }
        
        // If there are multiple options in the file, we should get more than one unique result
        // Note: This is a probabilistic test - it might fail if we're extremely unlucky
        Assert.True(results.Count > 1);
    }
}