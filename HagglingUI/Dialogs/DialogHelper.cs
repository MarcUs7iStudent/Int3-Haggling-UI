using System.Globalization;
using HagglingContracts.Interfaces;
using HagglingContracts.Models;

namespace HagglingUI.Dialogs;

public static class DialogHelper
{
    public static string FormatDialog(string dialog, IHuman vendor, IHuman customer, Product product, decimal vendorOffer, decimal customerOffer, CultureInfo? culture = null)
    {
        var cultureInfo = culture ?? CultureInfo.InvariantCulture;
        IVendor actualVendor = (IVendor)vendor;
        Dictionary<string, string> data = new Dictionary<string, string>
        {
            { "vendor_name", vendor.Name },
            { "vendor_age", vendor.Age.ToString() },
            { "vendor_offer", vendorOffer.ToString(cultureInfo) },
            { "vendor_selling_point", actualVendor.SellingPoint.ToString() },
            { "vendor_reputation", actualVendor.Reputation.ToString() },
            { "customer_name", customer.Name },
            { "customer_age", customer.Age.ToString() },
            { "customer_offer", customerOffer.ToString(cultureInfo) },
            { "item", product.Name }
        };
        
        string result = dialog;
        foreach (var pair in data)
        {
            result = result.Replace($"{{{pair.Key}}}", pair.Value);
        }
    
        return result;
    }
}