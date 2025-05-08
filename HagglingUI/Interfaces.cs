namespace Haggling;

public interface IHuman
{
    /// <summary>
    /// Age of the person
    /// </summary>
    ushort Age { get; }
    
    /// <summary>
    /// Current mood of the person
    /// </summary>
    Mood Mood { get; }
    
    /// <summary>
    /// Name of the person
    /// </summary>
    string Name { get; }
    
    /// <summary>
    /// Gender of the person
    /// </summary>
    Gender Gender { get; }
    
    /// <summary>
    /// Traits of this person
    /// </summary>
    HashSet<Trait> Traits { get; }
    
    /// <summary>
    /// The marketplace this vendor does business in
    /// </summary>
    IMarketplace? CurrentMarketplace { get; }
    
    /// <summary>
    /// Things owned by this person
    /// </summary>
    List<Product> Inventory { get; }
    
    /// <summary>
    /// Processes the offer given by a vendor based on its reasonableness and traits of the vendor,
    /// either accepting the offer, canceling the trade, or giving a counteroffer,
    /// also changing own mood accordingly 
    /// </summary>
    /// <param name="vendor"> The person offering </param>
    /// <param name="offer"> The offer to react to </param>
    /// <param name="counteroffer"> if so decided, give the other person a counteroffer, leave as null otherwise </param>
    /// <returns> decided answer of the customer </returns>
    Dialogue ProcessOffer(IHuman vendor, Offer offer, out Offer? counteroffer);
}

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
    Offer SuggestProduct(ICustomer customer, ProductType lookingToBuy);

    /// <summary>
    /// Adds the customer to a blacklist, after which he cannot try to buy from this vendor again
    /// </summary>
    /// <param name="customer"> customer to blacklist </param>
    void Blacklist(ICustomer customer);

    /// <summary>
    /// Removes the product from the inventory and adds the proceeds to the balance
    /// </summary>
    /// <param name="product"></param>
    void SellProduct(Product product);

    /// <summary>
    /// Greets a person or kicks them out of the store if they are in the blacklist
    /// </summary>
    /// <param name="person"> person to greet/kick out </param>
    /// <returns> Dialogue to print </returns>
    Dialogue InitiateTrade(ICustomer person);
}

public interface ICustomer : IHuman
{
    /// <summary>
    /// A title describing the person's status e.g. "Travelling merchant" or "Local peasant"
    /// </summary>
    string Title { get; }
    
    /// <summary>
    /// List of all the things the customer wants to buy
    /// </summary>
    List<ProductType> BuyList { get; }

    /// <summary>
    /// Selects the next vendor to buy things from based on their selling point and their local reputation
    /// from the current marketplace that he is in
    /// </summary>
    /// <returns> The vendor selected </returns>
    IVendor SelectVendor();

    /// <summary>
    /// Selects a product to trade for from the available inventory
    /// </summary>
    /// <param name="vendor"> the vendor to select products from</param>
    /// <returns> product selected </returns>
    Product SelectProduct(IVendor vendor);

    /// <summary>
    /// Asks for a product based on the buy list
    /// </summary>
    /// <returns> Product type asked for </returns>
    ProductType AskForProduct();

    /// <summary>
    /// Adds the products into own inventory and removes the necessary funds
    /// </summary>
    /// <param name="product"> product to add </param>
    void BuyProduct(Product product);
}

public interface IMarketplace
{
    /// <summary>
    /// Vendors current ly trading here
    /// </summary>
    List<IVendor> Vendors { get; }

    /// <summary>
    /// Starts a trade between two people 
    /// </summary>
    /// <param name="customer"> customer in the trade </param>
    /// <param name="vendor"> vendor in the trade </param>
    void StartTrade(ICustomer customer, IVendor vendor);
}