using BusinessService.Domain.Models;
using BusinessService.Domain.Services;

namespace BusinessService.Services
{
    public class QuoteService : IQuoteService
    {
        private readonly IQuoteRepository _quoteRepo;
        public QuoteService(IQuoteRepository quoteRepo)
        {
            _quoteRepo = quoteRepo;
        }
        public Quote CanUpdateQuote(int id, Quote quote)
        {
            var Quote = _quoteRepo.CanUpdateQuote(id, quote);
            if (Quote != null)
            {
                return Quote;
            }
            else return null;
        }
        public decimal CalculateMaturityAmount(Quote quote)
        {
            var maturityAmount = _quoteRepo.CalculateMaturityAmount(quote);
            return maturityAmount;
        }
    }
}
