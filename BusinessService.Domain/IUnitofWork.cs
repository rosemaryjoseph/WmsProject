using BusinessService.Domain.Services;

namespace BusinessService.Domain
{
    public interface IUnitofWork
    {
        ICustomerRepository CustomerRepo { get; }
        IQuoteRepository QuoteRepo { get; }
        int Complete();
    }
}
