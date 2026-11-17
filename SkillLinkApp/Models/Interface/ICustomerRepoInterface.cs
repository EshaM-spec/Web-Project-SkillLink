namespace SkillLinkApp.Models.Interface
{
    public interface ICustomerRepoInterface
    {
        public List<Customer> GetAllCustomers();
        public Customer GetCustomerById(string id);
        public void UpdateCustomer(Customer customer, string id);
        public void DeleteCustomer(string id);
        public void AddCustomer(Customer customer);
    }
}
