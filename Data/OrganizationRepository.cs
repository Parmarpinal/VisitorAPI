using Microsoft.Data.SqlClient;
using VisitorWebAPI.Models;

namespace VisitorWebAPI.Data
{
    public class OrganizationRepository
    {
        private readonly string _connectionString;
        public OrganizationRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }
        public IEnumerable<OrganizationModel> SelectAll()
        {
            var organizations = new List<OrganizationModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Organization_SelectAll", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    organizations.Add(new OrganizationModel
                    {
                        OrganizationID = Convert.ToInt32(reader["OrganizationID"]),
                        OrganizationName = reader["OrganizationName"].ToString(),
                        Head = reader["Head"].ToString(),
                        City = reader["City"].ToString(),
                        Address = reader["Address"].ToString(),
                        EstablishedDate = Convert.ToDateTime(reader["EstablishedDate"]),
                        ReceptionistCount = Convert.ToInt32(reader["ReceptionistCount"])
                    });
                }
            }
            return organizations;
        }

        public bool Add(OrganizationModel model)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Organization_Insert", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@OrganizationName", model.OrganizationName);
                cmd.Parameters.AddWithValue("@Head", model.Head);
                cmd.Parameters.AddWithValue("@City", model.City);
                cmd.Parameters.AddWithValue("@Address", model.Address);
                cmd.Parameters.AddWithValue("@EstablishedDate", model.EstablishedDate);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        public bool Edit(OrganizationModel model)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Organization_Update", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@OrganizationID", model.OrganizationID);
                cmd.Parameters.AddWithValue("@OrganizationName", model.OrganizationName);
                cmd.Parameters.AddWithValue("@Head", model.Head);
                cmd.Parameters.AddWithValue("@City", model.City);
                cmd.Parameters.AddWithValue("@Address", model.Address);
                cmd.Parameters.AddWithValue("@EstablishedDate", model.EstablishedDate);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        public bool Delete(int OrganizationID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Organization_Delete", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@OrganizationID", OrganizationID);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public OrganizationModel SelectByPK(int OrganizationID)
        {
            OrganizationModel model = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("PR_Organization_SelectByPK", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                };
                command.Parameters.AddWithValue("@OrganizationID", OrganizationID);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    model = new OrganizationModel
                    {
                        OrganizationID = Convert.ToInt32(reader["OrganizationID"]),
                        OrganizationName = reader["OrganizationName"].ToString(),
                        Head = reader["Head"].ToString(),
                        City = reader["City"].ToString(),
                        Address = reader["Address"].ToString(),
                        EstablishedDate = Convert.ToDateTime(reader["EstablishedDate"])
                    };
                }
            }
            return model;
        }
    }
}
