using BusinessService.Domain;
using BusinessService.Domain.Models;
using BusinessService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessService.CustomerApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly ICustomerService _custService;
        private readonly ILogger<CustomersController> _logger;
      
        public CustomersController(IUnitofWork unitOfWork, ICustomerService custService, ILogger<CustomersController> logger)
        {
            _unitOfWork = unitOfWork as UnitOfWork;
            _custService = custService;
            _logger = logger;
        }
       
        [HttpPost("AddCustomer")]
        [ApiVersion("2.0")]
        //[Authorize(Roles ="Admin")]
        public IActionResult AddCustomer([FromBody] Customer customer)
        {
            try
            {
                if (!_custService.CanAddCustomer(customer))
                {
                    if (!ModelState.IsValid)
                        return BadRequest();
                    _unitOfWork.CustomerRepo.Add(customer);
                    _unitOfWork.Complete();
                    return Ok("Customer with name " + customer.FirstName + " Added successfully.");
                }
                return BadRequest("Customer cannot be added.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Customer Addition Failed", ex);
                return BadRequest();
            }

        }

        [Route("GetAllCustomers")]
        [HttpGet, MapToApiVersion("1.0")]
        public List<Customer> GetAllCustomers()
        {
            try
            {
                var custList = _unitOfWork.CustomerRepo.GetAll();
                return custList.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all customer data", ex);
                return null;
            }
        }

        [HttpGet]
        [ApiVersion("1.0")]
        [Route("GetCustomerById/{id}")]
        public Customer GetCustomerById(int? id)
        {
            try
            {
                if (id.HasValue)
                {
                    var customerById = _unitOfWork.CustomerRepo.Get(id);
                    return customerById;
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get customer by {id} ", ex);
                return null;
            }

        }

        [HttpPut]
        [ApiVersion("2.0")]
        [Route("UpdateCustomer/{id}")]
        public IActionResult UpdateCustomer(int id, [FromBody] Customer customer)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();
                var updatedCustomer = _custService.CanUpdateCustomer(id, customer);
                if (updatedCustomer != null)
                {
                    _unitOfWork.CustomerRepo.Update(updatedCustomer);
                    _unitOfWork.Complete();
                    return Ok("Customer with ID " + id.ToString() + " Updated successfully.");
                }
                return BadRequest("Customer with ID " + id.ToString() + " not found in the database");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to update the customer", ex);
                return BadRequest();
            }

        }

        [ApiVersion("2.0")]
        [HttpDelete]
        [Route("DeleteCustomer/{id}")]
        public IActionResult Delete(int? id)
        {
            try
            {
                var deleteCustomer = _unitOfWork.CustomerRepo.Get(id);
                deleteCustomer.IsDeleted = true;
                int complete = _unitOfWork.Complete();
                if (complete > 0)
                    return Ok("Customer with Id " + id.ToString() + " deleted successfully.");
                return BadRequest("Customer with Id " + id.ToString() + " not found to delete");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to delete the customer", ex);
                return BadRequest();
            }
        }
    }
}

