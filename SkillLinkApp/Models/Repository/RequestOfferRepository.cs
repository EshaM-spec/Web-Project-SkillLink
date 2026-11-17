
using SkillLinkApp.Models;
using SkillLinkApp.Models.Interface;
using Dapper;
using Microsoft.Data.SqlClient;

namespace SkillLinkApp.Repositories
{
    public class RequestOfferRepository : IRequestOfferRepoInterface
    {



        static string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=aspnet-WebApplication2-03e8c327-76b5-47af-b7e0-2cdfe74bc1eb;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;Command Timeout=30";
        static SqlConnection con = new SqlConnection(connectionString);
        public void AddOffer(RequestOffer offer)
        {
            string query = @"
                INSERT INTO RequestOffers
                (RequestId, WorkerId, PriceOffer, ProposedTime,
                 EstimatedDuration, Message, Status)
                VALUES
                (@RequestId, @WorkerId, @PriceOffer, @ProposedTime,
                 @EstimatedDuration, @Message, @Status)";

            using (var con = new SqlConnection(connectionString))
            {
                con.Execute(query, offer);
            }
        }
        public List<CustomerOfferViewModel> GetOffersByRequestId(int requestId)
        {
            string query = @"
        SELECT
            ro.Id AS OfferId,
            ro.RequestId,
            w.FullName AS WorkerName,
            ro.PriceOffer,
            ro.ProposedTime,
            ro.EstimatedDuration,
            ro.Message,
            ro.Status
        FROM RequestOffers ro
        INNER JOIN Workers w ON ro.WorkerId = w.Id
        WHERE ro.RequestId = @RequestId
        ORDER BY ro.CreatedAt DESC";

            using (var con = new SqlConnection(connectionString))
            {
                return con.Query<CustomerOfferViewModel>(
                    query,
                    new { RequestId = requestId }
                ).ToList();
            }
        }
    }
}