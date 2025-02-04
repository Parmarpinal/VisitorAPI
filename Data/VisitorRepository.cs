using Microsoft.Data.SqlClient;
using VisitorWebAPI.Models;

namespace VisitorWebAPI.Data
{
    public class VisitorRepository
    {
        private readonly string _connectionString;
        public VisitorRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }

        public IEnumerable<VisitorModel> SelectAll()
        {
            var users = new List<VisitorModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Visitor_SelectAll", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    users.Add(new VisitorModel
                    {
                        VisitorID = Convert.ToInt32(reader["VisitorID"]),
                        UserID = Convert.ToInt32(reader["UserID"]),
                        VisitorName = reader["VisitorName"].ToString(),
                        OrganizationID = Convert.ToInt32(reader["OrganizationID"]),
                        OrganizationName = reader["OrganizationName"].ToString(),
                        DepartmentID = Convert.ToInt32(reader["DepartmentID"]),
                        DepartmentName = reader["DepartmentName"].ToString(),
                        Purpose = reader["Purpose"].ToString(),
                        HostID = Convert.ToInt32(reader["HostID"]),
                        HostName = reader["HostName"].ToString(),
                        NoOfPerson = Convert.ToInt32(reader["NoOfPerson"]),
                        IDProof = reader["IDProof"].ToString(),
                        IDProofNumber = reader["IDProofNumber"].ToString(),
                        VisitDate = Convert.ToDateTime(reader["VisitDate"]),
                        //CheckIn = TimeOnly.Parse(reader["CheckIn"].ToString()),
    //                    CheckIn = reader["CheckIn"] != DBNull.Value && !string.IsNullOrWhiteSpace(reader["CheckIn"].ToString())
    //? TimeOnly.Parse(reader["CheckIn"].ToString())
    //: null,
    //                    CheckOut = reader["CheckOut"] != DBNull.Value && !string.IsNullOrWhiteSpace(reader["CheckOut"].ToString())
    //? TimeOnly.Parse(reader["CheckOut"].ToString())
    //: null,
                        //CheckOut = TimeOnly.Parse(reader["CheckOut"].ToString()),
                        CheckIn = reader["CheckIn"] != DBNull.Value && !string.IsNullOrWhiteSpace(reader["CheckIn"].ToString())
    ? (TimeSpan)reader["CheckIn"]
    : null,
                        CheckOut = reader["CheckOut"] != DBNull.Value && !string.IsNullOrWhiteSpace(reader["CheckOut"].ToString())
                        ? (TimeSpan)reader["CheckOut"]
                        : null,
                    });
                }
            }
            return users;
        }

        public bool Add(VisitorModel model)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Visitor_Insert", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@UserID", model.UserID);
                cmd.Parameters.AddWithValue("@OrganizationID", model.OrganizationID);
                cmd.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                cmd.Parameters.AddWithValue("@Purpose", model.Purpose);
                cmd.Parameters.AddWithValue("@HostID", model.HostID);
                cmd.Parameters.AddWithValue("@NoOfPerson", model.NoOfPerson);
                cmd.Parameters.AddWithValue("@IDProof", model.IDProof);
                cmd.Parameters.AddWithValue("@IDProofNumber", model.IDProofNumber);
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        public bool EditToCheckedOut(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Visitor_UpdateCheckedOut", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@VisitorID", id);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        public bool Delete(int visitorID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Visitor_Delete", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@VisitorID", visitorID);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public VisitorModel SelectByPK(int visitorID)
        {
            VisitorModel model = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("PR_Visitor_SelectByPK", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                };
                command.Parameters.AddWithValue("@VisitorID", visitorID);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    model = new VisitorModel
                    {
                        VisitorID = Convert.ToInt32(reader["VisitorID"]),
                        UserID = Convert.ToInt32(reader["UserID"]),
                        OrganizationID = Convert.ToInt32(reader["OrganizationID"]),
                        DepartmentID = Convert.ToInt32(reader["DepartmentID"]),
                        Purpose = reader["Purpose"].ToString(),
                        HostID = Convert.ToInt32(reader["HostID"]),
                        NoOfPerson = Convert.ToInt32(reader["NoOfPerson"]),
                        IDProof = reader["IDProof"].ToString(),
                        IDProofNumber = reader["IDProofNumber"].ToString(),
                        VisitDate = Convert.ToDateTime(reader["VisitDate"]),
                        CheckIn = reader["CheckIn"] != DBNull.Value && !string.IsNullOrWhiteSpace(reader["CheckIn"].ToString())
    ? (TimeSpan)reader["CheckIn"]
    : null,
                        CheckOut = reader["CheckOut"] != DBNull.Value && !string.IsNullOrWhiteSpace(reader["CheckOut"].ToString())
                        ? (TimeSpan)reader["CheckOut"]
                        : null,
                    };
                }
            }
            return model;
        }

        public IEnumerable<VisitorModel> SelectByUserID(int userID)
        {
            var users = new List<VisitorModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Visitor_SelectByUserID", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@UserID", userID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    users.Add(new VisitorModel
                    {
                        VisitorID = Convert.ToInt32(reader["VisitorID"]),
                        OrganizationID = Convert.ToInt32(reader["OrganizationID"]),
                        OrganizationName = reader["OrganizationName"].ToString(),
                        DepartmentID = Convert.ToInt32(reader["DepartmentID"]),
                        DepartmentName = reader["DepartmentName"].ToString(),
                        Purpose = reader["Purpose"].ToString(),
                        HostID = Convert.ToInt32(reader["HostID"]),
                        HostName = reader["HostName"].ToString(),
                        NoOfPerson = Convert.ToInt32(reader["NoOfPerson"]),
                        IDProof = reader["IDProof"].ToString(),
                        IDProofNumber = reader["IDProofNumber"].ToString(),
                        VisitDate = Convert.ToDateTime(reader["VisitDate"]),
                        //CheckIn = TimeOnly.Parse(reader["CheckIn"].ToString()),
                        //                    CheckIn = reader["CheckIn"] != DBNull.Value && !string.IsNullOrWhiteSpace(reader["CheckIn"].ToString())
                        //? TimeOnly.Parse(reader["CheckIn"].ToString())
                        //: null,
                        //                    CheckOut = reader["CheckOut"] != DBNull.Value && !string.IsNullOrWhiteSpace(reader["CheckOut"].ToString())
                        //? TimeOnly.Parse(reader["CheckOut"].ToString())
                        //: null,
                        //CheckOut = TimeOnly.Parse(reader["CheckOut"].ToString()),
                        CheckIn = reader["CheckIn"] != DBNull.Value && !string.IsNullOrWhiteSpace(reader["CheckIn"].ToString())
    ? (TimeSpan)reader["CheckIn"]
    : null,
                        CheckOut = reader["CheckOut"] != DBNull.Value && !string.IsNullOrWhiteSpace(reader["CheckOut"].ToString())
                        ? (TimeSpan)reader["CheckOut"]
                        : null,
                    });
                }
            }
            return users;
        }
    }
}
