using HagglingContracts.Models;
namespace HagglingContracts.Interfaces;

public interface IVendor : IHuman
{
    /// <summary>
    /// What this vendor is famous for selling
    /// </summary>
    ProductType SellingPoint { get; }
    
    /// <summary>
    /// Reputation of this vendor
    /// </summary>
    Reputation Reputation { get; }

    /// <summary>
    /// Suggests a product to a customer based on what they are looking for and who they are
    /// </summary>
    /// <param name="customer"> customer to suggest to </param>
    /// <param name="lookingToBuy"> what the customer is looking to buy </param>
    /// <returns> Initial offer made for the suggestion </returns>
    Offer? SuggestProduct(ICustomer customer, ProductType lookingToBuy);

    /// <summary>
    /// Adds the customer to a blacklist, after which he cannot try to buy from this vendor again
    /// </summary>
    /// <param name="customer"> customer to blacklist </param>
    bool Blacklist(ICustomer customer);

    /// <summary>
    /// Removes the product from the inventory and adds the proceeds to the balance
    /// </summary>
    /// <param name="product">The product that gets selled</param>
    /// <param name="finalOffer">The final offer for the product that gets selled</param>
    bool SellProduct(Product product, Offer finalOffer);

    /// <summary>
    /// Greets a person or kicks them out of the store if they are in the blacklist
    /// </summary>
    /// <param name="person"> person to greet/kick out </param>
    /// <returns> Dialogue to print </returns>
    Dialogue? InitiateTrade(ICustomer person);
}