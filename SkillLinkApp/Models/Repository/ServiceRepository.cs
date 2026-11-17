
using SkillLinkApp.Models;
using SkillLinkApp.Models.Interface;
using Dapper;
using Microsoft.Data.SqlClient;

namespace SkillLinkApp.Models.Repository
{
    public class ServiceRepository : IServiceRepoInterface
    {
        private readonly string _connectionString;

        public ServiceRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // Get all services
        public List<Service> GetAllServices()
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Services";

                return con.Query<Service>(query).ToList();
            }
        }

        // Get service by ID
        public Service GetServiceById(string id)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Services WHERE Id = @Id";

                return con.QueryFirstOrDefault<Service>(
                    query, new { Id = int.Parse(id) });
            }
        }

        // Get services by category
        public List<Service> GetServicesByType(string serviceType)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string query = @"SELECT * FROM Services
                                 WHERE ServiceType = @ServiceType
                                 AND IsActive = 1";

                return con.Query<Service>(
                    query, new { ServiceType = serviceType }).ToList();
            }
        }

        // Add a new service
        public void AddService(Service service)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string query = @"INSERT INTO Services
                                 (ServiceName, ServiceType, IsActive)
                                 VALUES
                                 (@ServiceName, @ServiceType, @IsActive)";

                con.Execute(query, service);
            }
        }

        // Update an existing service
        public void UpdateService(Service service, string id)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string query = @"UPDATE Services
                                 SET ServiceName = @ServiceName,
                                     ServiceType = @ServiceType,
                                     IsActive = @IsActive
                                 WHERE Id = @Id";

                con.Execute(query, new
                {
                    service.ServiceName,
                    service.ServiceType,
                    service.IsActive,
                    Id = int.Parse(id)
                });
            }
        }

        // Delete a service
        public void DeleteService(string id)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Services WHERE Id = @Id";

                con.Execute(query, new { Id = int.Parse(id) });
            }
        }
    }
}
