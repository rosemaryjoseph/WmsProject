using BusinessService.Domain;
using BusinessService.Domain.Models;
using BusinessService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessService.QuoteApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [ApiController]
    public class QuotesController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IQuoteService _quoteService;
        private readonly ILogger<QuotesController> _logger;

        public QuotesController(IUnitofWork unitOfWork, IQuoteService quoteService, ILogger<QuotesController> logger)
        {
            _unitOfWork = unitOfWork as UnitOfWork;
            _quoteService = quoteService;
            _logger = logger;
        }

        [HttpPost("AddQuote")]
        [ApiVersion("2.0")]
        //[Authorize(Roles = "Admin")]
        public IActionResult AddQuote([FromBody] Quote quote)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();
                var customer = _unitOfWork.CustomerRepo.Get(quote.CustomerID);
                if (customer != null)
                {
                    var maturityAmount = _quoteService.CalculateMaturityAmount(quote);
                    quote.MaturityAmount = maturityAmount;
                    _unitOfWork.QuoteRepo.Add(quote);
                    _unitOfWork.Complete();
                    return Ok("Quote for customer id " + quote.CustomerID + " Added successfully.");
                }
                else return BadRequest("Customer for customer id " + quote.CustomerID + " does not exist ");

            }
            catch (Exception ex)
            {
                _logger.LogError($"Quote Creation Failed", ex);
                return BadRequest();
            }

        }

        [Route("GetAllQuotes")]
        [HttpGet, MapToApiVersion("1.0")]
        public List<Quote> GetAllQuotes()
        {
            try
            {
                var quoteList = _unitOfWork.QuoteRepo.GetAll();
                return quoteList.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all quotes data", ex);
                return null;
            }
        }

        [HttpGet]
        [ApiVersion("1.0")]
        [Route("GetQuoteById/{id}")]
        public Quote GetQuoteById(int? id)
        {
            try
            {
                if (id.HasValue)
                {
                    var QuoteById = _unitOfWork.QuoteRepo.Get(id);
                    return QuoteById;
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get quote by {id} ", ex);
                return null;
            }

        }

        [HttpPut]
        [ApiVersion("2.0")]
        [Route("UpdateQuote/{id}")]
        public IActionResult UpdateCustomer(int id, [FromBody] Quote quote)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();
                var updatedQuote = _quoteService.CanUpdateQuote(id, quote);
                if (updatedQuote != null)
                {
                    _unitOfWork.QuoteRepo.Update(updatedQuote);
                    _unitOfWork.Complete();
                    return Ok("Quote with ID " + id.ToString() + " Updated successfully.");
                }
                return BadRequest("Quote with ID " + id.ToString() + " not found in the database");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to update the quote", ex);
                return BadRequest();
            }

        }

        [ApiVersion("2.0")]
        [HttpDelete]
        [Route("DeleteQuote/{id}")]
        public IActionResult Delete(int? id)
        {
            try
            {
                var deleteQuote = _unitOfWork.QuoteRepo.Get(id);
                deleteQuote.IsDeleted = true;
                int complete = _unitOfWork.Complete();
                if (complete > 0)
                    return Ok("Quote with Id " + id.ToString() + " deleted successfully.");
                return BadRequest("Quote with Id " + id.ToString() + " not found to delete");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to delete the quote", ex);
                return BadRequest();
            }
        }
    }
}

