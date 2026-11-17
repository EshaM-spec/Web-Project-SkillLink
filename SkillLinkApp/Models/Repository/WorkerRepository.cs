using Dapper;
using Microsoft.Data.SqlClient;
using SkillLinkApp.Models.Interface;
using SkillLinkApp.Models;
namespace SkillLinkApp.Models.Repository
{
    public class WorkerRepository: IWorkerRepoInterface
    {
        static string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=aspnet-WebApplication2-03e8c327-76b5-47af-b7e0-2cdfe74bc1eb;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;Command Timeout=30";
        static SqlConnection con = new SqlConnection(connectionString);
       public void AddWorker(Worker worker)
        {
            string query = "INSERT INTO Workers (UserId, FullName, PhoneNumber, Address, City, Country, ServiceCategory) VALUES (@UserId, @FullName, @PhoneNumber, @Address, @City, @Country, @ServiceCategory)";
            con.Execute(query, worker);
            
            
        }
        public void DeleteWorker(string id)
        {
            string query = "Delete from Workers where userId=@Id";
            con.QueryFirstOrDefault<Worker>(query, new { Id = id });
      
        }
        public List<Worker> GetAllWorkers()
        {
            string query = "SELECT * FROM Workers";

            var workers = con.Query<Worker>(query).ToList();

            return workers;
        }
        public Worker GetWorkerById(string id)
        {
            string query = "Select * from Workers where userId=@Id";
            var worker = con.QueryFirstOrDefault<Worker>(query, new { Id= id });
            return worker;
        }
        public void UpdateWorker(Worker worker, string id)
        {
            string query = @"UPDATE Workers
                     SET FullName = @FullName,
                         PhoneNumber = @PhoneNumber,
                         Address = @Address,
                         City = @City,
                         Country = @Country,
                         ServiceCategory = @ServiceCategory
                     WHERE UserId = @Id";

            con.Execute(query, new
            {
                worker.FullName,
                worker.PhoneNumber,
                worker.Address,
                worker.City,
                worker.Country,
                worker.ServiceCategory,
                Id = id
            });
        }

    }
}
