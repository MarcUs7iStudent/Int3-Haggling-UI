namespace HagglingContracts.Interfaces;

public interface IMarketplace
{
    /// <summary>
    /// Vendors currently trading here
    /// </summary>
    List<IVendor> Vendors { get; }

    /// <summary>
    /// Starts a trade between two people 
    /// </summary>
    /// <param name="customer">Customer in the trade</param>
    /// <param name="vendor">Vendor in the trade</param>
    void StartTrade(ICustomer customer, IVendor vendor);
}