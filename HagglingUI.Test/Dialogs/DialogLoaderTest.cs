using System.Reflection;
using System.Text;
using HagglingUI.Dialogs;
using Moq;
using Xunit;

namespace HagglingUI.Test;

public class DialogLoaderTests
{
    [Fact]
    public void LoadDialogue_ValidJson_ReturnsDeserializedObject()
    {
        var mockAssembly = new Mock<Assembly>();
        var validJson = "{\"greeting\":{\"neutral\":[\"Hello\",\"Hi\"]}}";
        var mockStream = new MemoryStream(Encoding.UTF8.GetBytes(validJson));
        
        mockAssembly.Setup(a => a.GetManifestResourceStream("HagglingUI.Data.test-dialogue.json"))
            .Returns(mockStream);
        
        var result = DialogLoader.LoadDialogue<Dictionary<string, Dictionary<string, List<string>>>>("test-dialogue.json", mockAssembly.Object);
        
        Assert.NotNull(result);
        Assert.True(result.ContainsKey("greeting"));
        Assert.True(result["greeting"].ContainsKey("neutral"));
        Assert.Equal(2, result["greeting"]["neutral"].Count);
        Assert.Equal("Hello", result["greeting"]["neutral"][0]);
    }

    [Fact]
    public void LoadDialogue_ResourceNotFound_ReturnsDefault()
    {
        var result = DialogLoader.LoadDialogue<Dictionary<string, string>>("non-existent.json");
        
        Assert.Null(result);
    }

    [Fact]
    public void LoadDialogue_InvalidJson_ReturnsDefault()
    {
        var mockAssembly = new Mock<Assembly>();
        var invalidJson = "{invalid-json}";
        var mockStream = new MemoryStream(Encoding.UTF8.GetBytes(invalidJson));
        
        mockAssembly.Setup(a => a.GetManifestResourceStream("HagglingUI.Data.invalid.json"))
            .Returns(mockStream);
        
        var result = DialogLoader.LoadDialogue<Dictionary<string, Dictionary<string, List<string>>>>("invalid.json");
        
        Assert.Null(result);
    }

    [Fact]
    public void LoadAllDialogues_AllFilesExist_ReturnsTupleWithAllDialogues()
    {
        var mockAssembly = new Mock<Assembly>();
        var customerJson = "{\"greeting\":{\"friendly\":[\"Hello friend\"]}}";
        var vendorJson = "{\"greeting\":{\"business\":[\"Welcome to my shop\"]}}";
        var errorJson = "{\"error\":{\"general\":[\"Something went wrong\"]}}";
        
        mockAssembly.Setup(a => a.GetManifestResourceStream("HagglingUI.Data.customer-dialog.json"))
            .Returns(new MemoryStream(Encoding.UTF8.GetBytes(customerJson)));
        mockAssembly.Setup(a => a.GetManifestResourceStream("HagglingUI.Data.vendor-dialog.json"))
            .Returns(new MemoryStream(Encoding.UTF8.GetBytes(vendorJson)));
        mockAssembly.Setup(a => a.GetManifestResourceStream("HagglingUI.Data.error-dialog.json"))
            .Returns(new MemoryStream(Encoding.UTF8.GetBytes(errorJson)));
        
        var (customer, vendor, error) = DialogLoader.LoadAllDialogues(mockAssembly.Object);
        
        Assert.NotNull(customer);
        Assert.NotNull(vendor);
        Assert.NotNull(error);
        Assert.True(customer.ContainsKey("greeting"));
        Assert.True(vendor.ContainsKey("greeting"));
        Assert.True(error.ContainsKey("error"));
    }
}
