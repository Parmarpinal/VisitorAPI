using Microsoft.Data.SqlClient;
using VisitorWebAPI.Models;

namespace VisitorWebAPI.Data
{
    public class NotificationRepository
    {
        private readonly string _connectionString;
        public NotificationRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }

        public IEnumerable<NotificationModel> SelectByHostID(int id)
        {
            var notifications = new List<NotificationModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Notification_SelectByHostID", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@hostID", id);  
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    notifications.Add(new NotificationModel
                    {
                        NotificationID = Convert.ToInt32(reader["NotificationID"]),
                        HostID = Convert.ToInt32(reader["HostID"]),
                        Message = reader["Message"].ToString(),
                        VisitorID = Convert.ToInt32(reader["VisitorID"]),
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                        IsRead = Convert.ToBoolean(reader["IsRead"]),
                    });
                }
            }
            return notifications;
        }

        public bool Add(NotificationModel model)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Notification_Insert", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@HostID", model.HostID);
                cmd.Parameters.AddWithValue("@Message", model.Message);
                cmd.Parameters.AddWithValue("@VisitorID", model.VisitorID);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public bool Edit(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Notification_UpdateToRead", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@NotificationID", id);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
    }
}
