using BusinessService.CustomerApi;
using BusinessService.CustomerApi.Controllers;
using BusinessService.Domain;
using BusinessService.Domain.Models;
using BusinessService.Domain.Services;
using BusinessService.Services;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace BusinessService.UnitTests
{
    public class CustomersServiceTest : BaseUnitTest
    {


        private readonly Mock<ICustomerService> _custService;
        private readonly Mock<ICustomerRepository> _custRepository;
        private readonly Mock<IUnitofWork> _iunitOfWork;
        private readonly Mock<UnitOfWork> _unitOfWork;
        private readonly Mock<CustomersController> _custControl;
        private readonly Mock<ILogger<CustomersController>> _logger;
        private CustomersController _target;

        public CustomersServiceTest()
        {
            _custService = MockBaseRepository.Create<ICustomerService>();
            _custRepository = MockBaseRepository.Create<ICustomerRepository>();
            _custControl = MockBaseRepository.Create<CustomersController>();
            _iunitOfWork = MockBaseRepository.Create<IUnitofWork>();
            _unitOfWork = MockBaseRepository.Create<UnitOfWork>();
            _logger = MockBaseRepository.Create<ILogger<CustomersController>>();
            _target = new CustomersController(_iunitOfWork.Object as UnitOfWork, _custService.Object, _logger.Object);
        }

        [Fact]
        public void CustomerService_Given_CustomerId_Should_Get_Customer_Name()
        {
            //Arrange
            var customerId = 1;
            var expected = "Elvin";
            var customer = new Customer() { FirstName = expected, CustomerID = customerId };

            _custRepository.Setup(m => m.Get(customerId)).Returns(customer).Verifiable();
             _iunitOfWork.Setup(m => m.CustomerRepo).Returns(_custRepository.Object);

        }   
    }

}
