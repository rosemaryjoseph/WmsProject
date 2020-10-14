using BusinessService.Data;
using BusinessService.Domain.Models;
using BusinessService.Domain.Services;
using System;
using System.Linq;

namespace BusinessService.Services.Repositories
{
    public class QuoteRepository : GenericRepository<Quote>, IQuoteRepository
    {
        private readonly BusinessServiceDbContext _context;
        public QuoteRepository(BusinessServiceDbContext context) : base(context)
        {
            _context = context;
        }

        public Quote CanUpdateQuote(int id, Quote quote)
        {
            var quoteDetails = _context.Quotes.FirstOrDefault(x => x.QuoteID == id);
            if (quoteDetails != null)
            {
                quoteDetails.StartDate = quote.StartDate;
                quoteDetails.EndDate = quote.EndDate;
                quoteDetails.CustomerID = quote.CustomerID;
                quoteDetails.ContributionAmount = quote.ContributionAmount;
                var maturityaAmount = CalculateMaturityAmount(quote);
                quoteDetails.MaturityAmount = maturityaAmount;
                return quoteDetails;
            }
            return null;

        }
        public decimal CalculateMaturityAmount(Quote quote)
        {
            var totalDays = Convert.ToInt32((quote.EndDate - quote.StartDate).TotalDays);
            var rateofInterest = GetROI(totalDays);
            quote.MaturityAmount = quote.ContributionAmount + quote.ContributionAmount * Convert.ToDecimal(rateofInterest / 100);
            return quote.MaturityAmount;
        }

        public double GetROI(int totalDays)
        {
            var rateofInterest = 0.0;
            if (totalDays <= 30)
                rateofInterest = 0.5;
            else if (totalDays > 30 && totalDays <= 90)
                rateofInterest = 1.5;
            else if (totalDays > 90 && totalDays <= 120)
                rateofInterest = 2;
            else rateofInterest = 5;
            return rateofInterest;
        }
    }

}
