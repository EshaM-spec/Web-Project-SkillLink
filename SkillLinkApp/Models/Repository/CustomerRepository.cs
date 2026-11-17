using Dapper;
using Microsoft.Data.SqlClient;
using SkillLinkApp.Models;
using SkillLinkApp.Models.Interface;
namespace SkillLinkApp.Models.Repository
{
    public class CustomerRepository:ICustomerRepoInterface
    {
        static string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=aspnet-WebApplication2-03e8c327-76b5-47af-b7e0-2cdfe74bc1eb;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;Command Timeout=30";
        static SqlConnection con = new SqlConnection(connectionString);
        public List<Customer> GetAllCustomers()
        {
            string query = "Select * from  Customers";

            var customers = con.Query<Customer>(query).ToList();

            return customers;
        }
        public Customer GetCustomerById(string id)
        {
            string query = "Select * from Customers where userId=@Id";
            var customer = con.QueryFirstOrDefault<Customer>(query, new { Id = id });
            return customer;
        }
        public void UpdateCustomer(Customer customer, string id)
        {

            string query = @"UPDATE Customers
                     SET FullName = @FullName,
                         PhoneNumber = @PhoneNumber,
                         Address = @Address,
                         City = @City,
                         Country = @Country,
                     WHERE UserId = @Id";

            con.Execute(query, new
            {
                customer.FullName,
                customer.PhoneNumber,
                customer.Address,
                customer.City,
                customer.Country,
                Id = id
            });
        }
        public void DeleteCustomer(string id)
        {

            string query = "Delete from Customers where userId=@Id";
            con.QueryFirstOrDefault<Customer>(query, new { Id = id });

        }
        public void AddCustomer(Customer customer)
        {
            string query = "INSERT INTO Customers (UserId, FullName, PhoneNumber, Address, City, Country) VALUES (@UserId, @FullName, @PhoneNumber, @Address, @City, @Country)";
            con.Execute(query, customer);
        }
    }
}
