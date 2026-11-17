using Dapper;
using Microsoft.Data.SqlClient;
using SkillLinkApp.Models.Interface;
namespace SkillLinkApp.Models.Repository
{
    public class RequestRepository : IRequestRepoInterface
    {
        static string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=aspnet-WebApplication2-03e8c327-76b5-47af-b7e0-2cdfe74bc1eb;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;Command Timeout=30";
        static SqlConnection con = new SqlConnection(connectionString);

        public List<ServiceRequest> GetAllRequests()
        {
            string query = "Select * from  ServiceRequests";

            var requests = con.Query<ServiceRequest>(query).ToList();

            return requests;


        }
        public ServiceRequest GetRequestById(string id)
        {
            string query = @"
        SELECT
            sr.*,
            s.ServiceName
        FROM ServiceRequests sr
        INNER JOIN Services s
            ON sr.ServiceId = s.Id
        WHERE sr.Id = @Id";

            using (var con = new SqlConnection(connectionString))
            {
                return con.QueryFirstOrDefault<ServiceRequest>(query, new { Id = id });
            }
        }
        public void UpdateRequest(ServiceRequest request, string id)
        {

            string query = @"UPDATE ServiceRequests
                                 SET ServiceId = @ServiceId,
                                     Description = @Description,
                                     ServiceAddress = @ServiceAddress,
                                     UpdatedAt = GETDATE()
                                 WHERE Id = @Id";

            con.Execute(query, new
            {
                request.ServiceId,
                request.Description,
                request.ServiceAddress,
                Id = int.Parse(id)
            });

        }
        public void UpdateRequestStatus(string requestStatus, string id)
        {

            string query = @"UPDATE ServiceRequests
                                 SET Status = @Status,
                                     UpdatedAt = GETDATE()
                                 WHERE Id = @Id";

            con.Execute(query, new
            {
                Status = requestStatus,
                Id = int.Parse(id)
            });

        }
        public void DeleteRequest(string id)
        {
            string query = "Delete from ServiceRequests where userId=@Id";
            con.QueryFirstOrDefault<ServiceRequest>(query, new { Id = id });

        }
        public void AddRequest(ServiceRequest request)
        {
            string query = @"INSERT INTO ServiceRequests
                                 (CustomerId, ServiceId, Description,
                                  ServiceAddress, Status)
                                 VALUES
                                 (@CustomerId, @ServiceId, @Description,
                                  @ServiceAddress, @Status)";

            con.Execute(query, request);
        }
        public List<BookingViewModel> GetBookingsByCustomerId(int customerId)
        {
            string query = @"
        SELECT
            sr.Id AS RequestId,
            s.ServiceName,
            sr.Description,
            sr.ServiceAddress,
            sr.Status,
            sr.CreatedAt,
            sr.CompletedAt,
            w.FullName AS WorkerName
        FROM ServiceRequests sr
        INNER JOIN Services s ON sr.ServiceId = s.Id
        LEFT JOIN RequestOffers ro
            ON sr.Id = ro.RequestId
            AND ro.Status = 'Accepted'
        LEFT JOIN Workers w ON ro.WorkerId = w.Id
        WHERE sr.CustomerId = @CustomerId
        ORDER BY sr.CreatedAt DESC";

            using (var con = new SqlConnection(connectionString))
            {
                return con.Query<BookingViewModel>(
                    query,
                    new { CustomerId = customerId }
                ).ToList();
            }
        }
        public List<ProviderRequestViewModel> GetPendingRequestsByService(string serviceCategory)
        {
            string query = @"
    SELECT
        sr.Id AS RequestId,
        s.ServiceName,
        sr.Description,
        sr.ServiceAddress,
        sr.PreferredDate,
        sr.PreferredTime,
        sr.CreatedAt,
        c.City AS CustomerCity,
        c.Country AS CustomerCountry
    FROM ServiceRequests sr
    INNER JOIN Services s
        ON sr.ServiceId = s.Id
    INNER JOIN Customers c
        ON sr.CustomerId = c.Id
    WHERE
        sr.Status = 'Pending'
        AND s.ServiceName = @ServiceCategory
    ORDER BY sr.CreatedAt DESC";

            using (var con = new SqlConnection(connectionString))
            {
                return con.Query<ProviderRequestViewModel>(
                    query,
                    new { ServiceCategory = serviceCategory }
                ).ToList();
            }
        }

    }
}
