using eShop.CoreBusiness.Models;
using eShop.UseCases.PluginInterfaces.DataStore;
using System.Collections.Generic;

namespace eShop.UseCases.Services
{
    public class CustomerService
    {
        // Sử dụng kiểu Interface với Setter Injection
        public ICustomerDataAccess CustDataAccess { get; set; }

        public CustomerService()
        {
        }

        public CustomerService(ICustomerDataAccess custDA)
        {
            CustDataAccess = custDA;
        }

        public IEnumerable<Customer> GetCustomers()
        {
            // Gọi thông qua interface CustDataAccess được inject từ ngoài vào
            return CustDataAccess?.GetCustomers();
        }
    }
}
