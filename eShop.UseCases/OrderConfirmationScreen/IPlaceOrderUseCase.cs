using eShop.CoreBusiness.Models;
using System.Threading.Tasks;

namespace eShop.UseCases.OrderConfirmationScreen
{
    public interface IPlaceOrderUseCase
    {
        Task<string> Execute(Order order);
    }
}
