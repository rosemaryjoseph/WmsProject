using BusinessService.Domain.Models;

namespace BusinessService.Services
{
    public interface ICustomerService
    {
        bool CanAddCustomer(Customer customer);
        Customer CanUpdateCustomer(int id, Customer customer);
    }
}
