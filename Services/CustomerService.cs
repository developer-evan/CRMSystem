using crm_app.Models;
using crm_app.Repositories;
using System.ComponentModel.DataAnnotations;

namespace crm_app.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _customerRepository.GetAllAsync();
        }

        public async Task<Customer?> GetCustomerByIdAsync(int accountId)
        {
            return await _customerRepository.GetByIdAsync(accountId);
        }

        public async Task<(bool Success, string Message, Customer? Customer)> CreateCustomerAsync(Customer customer)
        {
            // Validate the customer model
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(customer);
            
            if (!Validator.TryValidateObject(customer, validationContext, validationResults, true))
            {
                var errors = string.Join("; ", validationResults.Select(v => v.ErrorMessage));
                return (false, $"Validation failed: {errors}", null);
            }

            // Check for duplicate email
            if (await _customerRepository.EmailExistsAsync(customer.Email))
            {
                return (false, "A customer with this email already exists.", null);
            }

            try
            {
                var createdCustomer = await _customerRepository.CreateAsync(customer);
                return (true, "Customer created successfully.", createdCustomer);
            }
            catch (Exception ex)
            {
                return (false, $"Error creating customer: {ex.Message}", null);
            }
        }

        public async Task<(bool Success, string Message, Customer? Customer)> UpdateCustomerAsync(Customer customer)
        {
            // Validate the customer model
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(customer);
            
            if (!Validator.TryValidateObject(customer, validationContext, validationResults, true))
            {
                var errors = string.Join("; ", validationResults.Select(v => v.ErrorMessage));
                return (false, $"Validation failed: {errors}", null);
            }

            // Check for duplicate email (excluding current customer)
            if (await _customerRepository.EmailExistsAsync(customer.Email, customer.AccountId))
            {
                return (false, "Another customer with this email already exists.", null);
            }

            try
            {
                var updatedCustomer = await _customerRepository.UpdateAsync(customer);
                return (true, "Customer updated successfully.", updatedCustomer);
            }
            catch (Exception ex)
            {
                return (false, $"Error updating customer: {ex.Message}", null);
            }
        }

        public async Task<(bool Success, string Message)> DeleteCustomerAsync(int accountId)
        {
            var customer = await _customerRepository.GetByIdAsync(accountId);
            if (customer == null)
            {
                return (false, "Customer not found.");
            }

            try
            {
                var deleted = await _customerRepository.DeleteAsync(accountId);
                if (deleted)
                {
                    return (true, "Customer deleted successfully.");
                }
                return (false, "Failed to delete customer.");
            }
            catch (Exception ex)
            {
                return (false, $"Error deleting customer: {ex.Message}");
            }
        }
    }
}
