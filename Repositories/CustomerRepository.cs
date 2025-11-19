using Microsoft.EntityFrameworkCore;
using crm_app.Data;
using crm_app.Models;

namespace crm_app.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers
                .AsNoTracking()
                .OrderByDescending(c => c.DateCreated)
                .ToListAsync();
        }

        public async Task<Customer?> GetByIdAsync(int accountId)
        {
            return await _context.Customers
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.AccountId == accountId);
        }

        public async Task<Customer?> GetByEmailAsync(string email)
        {
            return await _context.Customers
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<Customer> CreateAsync(Customer customer)
        {
            customer.DateCreated = DateTime.UtcNow;
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> UpdateAsync(Customer customer)
        {
            var existingCustomer = await _context.Customers
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.AccountId == customer.AccountId);
            
            if (existingCustomer == null)
                throw new InvalidOperationException("Customer not found");

            // Preserve the DateCreated from the existing customer
            customer.DateCreated = existingCustomer.DateCreated;
            
            // Detach any tracked entities with the same key
            var trackedEntity = _context.ChangeTracker.Entries<Customer>()
                .FirstOrDefault(e => e.Entity.AccountId == customer.AccountId);
            if (trackedEntity != null)
            {
                trackedEntity.State = EntityState.Detached;
            }

            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<bool> DeleteAsync(int accountId)
        {
            var customer = await GetByIdAsync(accountId);
            if (customer == null)
                return false;

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EmailExistsAsync(string email, int? excludeAccountId = null)
        {
            var query = _context.Customers.Where(c => c.Email == email);
            
            if (excludeAccountId.HasValue)
            {
                query = query.Where(c => c.AccountId != excludeAccountId.Value);
            }

            return await query.AnyAsync();
        }
    }
}
