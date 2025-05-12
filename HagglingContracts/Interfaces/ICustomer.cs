using HagglingContracts.Models;
namespace HagglingContracts.Interfaces;

public interface ICustomer : IHuman
{
    /// <summary>
    /// List of all the things the customer wants to buy
    /// </summary>
    List<ProductType> BuyList { get; }

    /// <summary>
    /// Selects the next vendor to buy things from based on their selling point and their local reputation
    /// from the current marketplace that he is in
    /// </summary>
    /// <returns> The vendor selected </returns>
    IVendor? SelectVendor();
    /// <summary>
    /// Selects a product to trade for from the available inventory
    /// </summary>
    /// <param name="vendor"> the vendor to select products from</param>
    /// <returns> product selected </returns>
    Product? SelectProduct(IVendor vendor);
    /// <summary>
    /// Asks for a product based on the buy list
    /// </summary>
    /// <returns> Product type asked for </returns>
    ProductType? AskForProduct();
    /// <summary>
    /// Adds the products into own inventory and removes the necessary funds
    /// </summary>
    /// <param name="product"> product to add </param>
    bool BuyProduct(Product product, IVendor vendor);
}