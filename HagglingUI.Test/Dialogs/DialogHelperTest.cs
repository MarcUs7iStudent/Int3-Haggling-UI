using System.Globalization;
using HagglingContracts.Interfaces;
using HagglingContracts.Models;
using HagglingUI.Dialogs;
using Moq;
using Xunit;

namespace HagglingUI.Test.Dialogs;

public class DialogHelperTest
{
    [Fact]
    public void FormatDialog_ReplacesAllPlaceholders()
    {
        // Arrange
        var mockVendor = CreateMockVendor("John", 45, ProductType.Tool, Reputation.Trustworthy);
        var mockCustomer = CreateMockCustomer("Alice", 30);
        var product = new Product(1, "Hammer", 10.0m, ProductType.Tool);
        
        string dialog = "Hello {customer_name}, I'm {vendor_name}. This {item} costs {vendor_offer} coins, not {customer_offer}.";
        
        // Act
        var result = DialogHelper.FormatDialog(
            dialog, mockVendor.Object, mockCustomer.Object, product, 10.0m, 5.0m);
        
        // Assert
        string expected = "Hello Alice, I'm John. This Hammer costs 10.0 coins, not 5.0.";
        Assert.Equal(expected, result);
    }
    
    [Fact]
    public void FormatDialog_WithDifferentCulture_FormatsNumbersCorrectly()
    {
        // Arrange
        var mockVendor = CreateMockVendor("Hans", 50, ProductType.Food, Reputation.Upright);
        var mockCustomer = CreateMockCustomer("Franz", 25);
        var product = new Product(1, "Bread", 3.5m, ProductType.Food);
        var germanCulture = new CultureInfo("de-DE");
        
        string dialog = "The {item} costs {vendor_offer} coins.";
        
        // Act
        var result = DialogHelper.FormatDialog(
            dialog, mockVendor.Object, mockCustomer.Object, product, 3.5m, 2.0m, germanCulture);
        
        // Assert
        string expected = "The Bread costs 3,5 coins.";
        Assert.Equal(expected, result);
    }
    
    [Fact]
    public void FormatDialog_WithMissingPlaceholders_KeepsPlaceholdersIntact()
    {
        // Arrange
        var mockVendor = CreateMockVendor("Bob", 40, ProductType.Clothing, Reputation.Sly);
        var mockCustomer = CreateMockCustomer("Carol", 35);
        var product = new Product(1, "Shirt", 20.0m, ProductType.Clothing);
        
        string dialog = "This is a {nonexistent_placeholder} in the text.";
        
        // Act
        var result = DialogHelper.FormatDialog(
            dialog, mockVendor.Object, mockCustomer.Object, product, 20.0m, 15.0m);
        
        // Assert
        Assert.Equal(dialog, result);
    }
    
    [Fact]
    public void FormatDialog_WithSpecialCharacters_ReplacesCorrectly()
    {
        // Arrange
        var mockVendor = CreateMockVendor("O'Malley", 55, ProductType.Luxury, Reputation.Deceitful);
        var mockCustomer = CreateMockCustomer("Smith-Jones", 28);
        var product = new Product(1, "Diamond Ring $$$", 1000.0m, ProductType.Luxury);
        
        string dialog = "{vendor_name} offers {item} to {customer_name}.";
        
        // Act
        var result = DialogHelper.FormatDialog(
            dialog, mockVendor.Object, mockCustomer.Object, product, 1000.0m, 800.0m);
        
        // Assert
        string expected = "O'Malley offers Diamond Ring $$$ to Smith-Jones.";
        Assert.Equal(expected, result);
    }
    
    [Fact]
    public void FormatDialog_WithEmptyDialog_ReturnsEmptyString()
    {
        // Arrange
        var mockVendor = CreateMockVendor("Emma", 30, ProductType.Dishware, Reputation.Unknown);
        var mockCustomer = CreateMockCustomer("David", 40);
        var product = new Product(1, "Plate", 5.0m, ProductType.Dishware);
        
        string dialog = "";
        
        // Act
        var result = DialogHelper.FormatDialog(
            dialog, mockVendor.Object, mockCustomer.Object, product, 5.0m, 3.0m);
        
        // Assert
        Assert.Equal("", result);
    }
    
    private Mock<IHuman> CreateMockCustomer(string name, ushort age)
    {
        var mockCustomer = new Mock<IHuman>();
        mockCustomer.Setup(c => c.Name).Returns(name);
        mockCustomer.Setup(c => c.Age).Returns(age);
        return mockCustomer;
    }
    
    private Mock<IVendor> CreateMockVendor(string name, ushort age, ProductType sellingPoint, Reputation reputation)
    {
        var mockVendor = new Mock<IVendor>();
        mockVendor.Setup(v => v.Name).Returns(name);
        mockVendor.Setup(v => v.Age).Returns(age);
        mockVendor.Setup(v => v.SellingPoint).Returns(sellingPoint);
        mockVendor.Setup(v => v.Reputation).Returns(reputation);
        return mockVendor;
    }
}