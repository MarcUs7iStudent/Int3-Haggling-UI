namespace HagglingContracts.Interfaces;

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