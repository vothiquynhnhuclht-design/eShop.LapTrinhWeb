using eShop.CoreBusiness.Models;
using System.Collections.Generic;

namespace eShop.UseCases.PluginInterfaces.DataStore
{
    public interface ICustomerDataAccess
    {
        IEnumerable<Customer> GetCustomers();
    }
}
