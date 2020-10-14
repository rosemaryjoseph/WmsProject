using BusinessService.Domain.Models;

namespace BusinessService.Domain.Services
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Customer GetUserByEmailId(string emailid);
        Customer CanUpdateCustomer(int id, Customer customer);
    }
}
