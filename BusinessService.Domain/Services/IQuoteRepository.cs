using BusinessService.Domain.Models;

namespace BusinessService.Domain.Services
{
    public interface IQuoteRepository : IRepository<Quote>
    {
        Quote CanUpdateQuote(int id, Quote quote);
        decimal CalculateMaturityAmount(Quote quote);
    }
}

