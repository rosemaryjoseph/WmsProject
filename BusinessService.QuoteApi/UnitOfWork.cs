using BusinessService.Data;
using BusinessService.Domain;
using BusinessService.Domain.Services;
using BusinessService.Services.Repositories;
using System;

namespace BusinessService.QuoteApi
{
    public class UnitOfWork : IUnitofWork
    {
        private readonly BusinessServiceDbContext _context;
        public ICustomerRepository CustomerRepo { get; private set; }
        public IQuoteRepository QuoteRepo { get; private set; }

        public UnitOfWork(BusinessServiceDbContext context)
        {
            this._context = context;
            CustomerRepo = new CustomerRepository(_context);
            QuoteRepo = new QuoteRepository(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
