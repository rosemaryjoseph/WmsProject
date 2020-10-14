using BusinessService.Domain.Models;
using BusinessService.Domain.Services;

namespace BusinessService.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _custRepo;

        public CustomerService(ICustomerRepository custRepo)
        {
            _custRepo = custRepo;
        }

        public bool CanAddCustomer(Customer customer)
        {
            var isCustomer = _custRepo.GetUserByEmailId(customer.Email);
            if (isCustomer != null)
            {
                return true;
            }
            return false;
        }

        public Customer CanUpdateCustomer(int id, Customer customer)
        {
            var Customer = _custRepo.CanUpdateCustomer(id, customer);
            if (Customer != null)
            {
                return Customer;
            }
            else return null;
        }
    }
}
