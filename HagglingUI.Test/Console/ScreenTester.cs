using HagglingContracts.Interfaces;
using HagglingContracts.Models;
using HagglingUI.Screen;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace HagglingUI.Test.Console;

public class ScreenTester
{
    private readonly ITestOutputHelper _testOutputHelper;

    public ScreenTester(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void TestPrintPersonInfo()
    {
        var mockPerson = CreateTestPerson();
        var screen = new Screen.Screen();
        
        using var sw = new StringWriter();
        System.Console.SetOut(sw);
    
        screen.PrintPersonInfo(mockPerson.Object);
        
        string output = sw.ToString();
        System.Console.SetOut(new StreamWriter(System.Console.OpenStandardOutput()) { AutoFlush = true });
        
        System.Console.WriteLine(output);

        Assert.Contains("Alice Trader", output);
    }
    
    private static Mock<IHuman> CreateTestPerson()
    {
        var mockPerson = new Mock<IHuman>();
        mockPerson.Setup(p => p.Name).Returns("Alice Trader");
        mockPerson.Setup(p => p.Age).Returns(32);
        mockPerson.Setup(p => p.Gender).Returns(Gender.Female);
        mockPerson.Setup(p => p.Mood).Returns(Mood.Happy);
        mockPerson.Setup(p => p.Funds).Returns(500.75m);
        
        var inventory = new List<Product>
        {
            new Product(1, "Golden Ring", 150.0m, ProductType.Luxury),
            new Product(2, "Leather Boots", 45.50m, ProductType.Clothing),
            new Product(3, "Silver Spoon", 25.0m, ProductType.Dishware)
        };
        mockPerson.Setup(p => p.Inventory).Returns(inventory);
        mockPerson.Setup(p => p.CurrentMarketplace).Returns((IMarketplace?)null);
        
        return mockPerson;
    }

    [Fact]
    public void TestPrintInitialQuestion()
    {
        var mockCustomer = CreateTestPerson();
        var mockVendor = CreateTestPerson();
        var screen = new Screen.Screen();
        
        using var sw = new StringWriter();
        System.Console.SetOut(sw);
    
        screen.PrintInitialQuestion(mockCustomer.Object, mockVendor.Object, ProductType.Tool);
        
        string output = sw.ToString();
        System.Console.SetOut(new StreamWriter(System.Console.OpenStandardOutput()) { AutoFlush = true });
        
        System.Console.WriteLine(output);

        Assert.Contains("Do you have any tools for sale?", output);
    }
}