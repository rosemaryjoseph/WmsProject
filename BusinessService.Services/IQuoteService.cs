using BusinessService.Domain.Models;

namespace BusinessService.Services
{
    public interface IQuoteService
    {
        decimal CalculateMaturityAmount(Quote quote);
        Quote CanUpdateQuote(int id, Quote quote);
    }
}
