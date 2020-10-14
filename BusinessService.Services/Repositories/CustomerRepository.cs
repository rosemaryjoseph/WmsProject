using BusinessService.Data;
using BusinessService.Domain.Models;
using BusinessService.Domain.Services;
using System.Linq;

namespace BusinessService.Services.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        private readonly BusinessServiceDbContext _context;
        public CustomerRepository(BusinessServiceDbContext context) : base(context)
        {
            _context = context;
        }
        public Customer GetUserByEmailId(string emailid)
        {
            return _context.Customers.FirstOrDefault(x => x.Email == emailid);
        }

        public Customer CanUpdateCustomer(int id, Customer customer)
        {
            var customerDetails = _context.Customers.FirstOrDefault(x => x.CustomerID == id);
            if (customerDetails != null)
            {
                customerDetails.FirstName = customer.FirstName;
                customerDetails.UserName = customer.UserName;
                customerDetails.PassWord = customer.PassWord;
                customerDetails.Address = customer.Address;
                customerDetails.State = customer.State;
                customerDetails.Country = customer.Country;
                customerDetails.Email = customer.Email;
                customerDetails.PAN = customer.PAN;
                customerDetails.ContactNo = customer.ContactNo;
                customerDetails.DOB = customer.DOB;
                customerDetails.AccountType = customer.AccountType;
                return customerDetails;
            }
            return null;
        }
    }
  }
