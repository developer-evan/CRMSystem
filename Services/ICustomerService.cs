using crm_app.Models;

namespace crm_app.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer?> GetCustomerByIdAsync(int accountId);
        Task<(bool Success, string Message, Customer? Customer)> CreateCustomerAsync(Customer customer);
        Task<(bool Success, string Message, Customer? Customer)> UpdateCustomerAsync(Customer customer);
        Task<(bool Success, string Message)> DeleteCustomerAsync(int accountId);
    }
}
