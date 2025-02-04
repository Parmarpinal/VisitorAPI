using Microsoft.Data.SqlClient;
using VisitorWebAPI.Models;

namespace VisitorWebAPI.Data
{
    public class DepartmentRepository
    {
        private readonly string _connectionString;
        public DepartmentRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }
        public IEnumerable<DepartmentModel> SelectAll()
        {
            var Departments = new List<DepartmentModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Department_SelectAll", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Departments.Add(new DepartmentModel
                    {
                        DepartmentID = Convert.ToInt32(reader["DepartmentID"]),
                        DepartmentName = reader["DepartmentName"].ToString(),
                        OrganizationID = Convert.ToInt32(reader["OrganizationID"]),
                        OrganizationName = reader["OrganizationName"].ToString(),
                        HostCount = Convert.ToInt32(reader["HostCount"])
                    });
                }
            }
            return Departments;
        }

        public bool Add(DepartmentModel model)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Department_Insert", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@DepartmentName", model.DepartmentName);
                cmd.Parameters.AddWithValue("@OrganizationID", model.OrganizationID);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        public bool Edit(DepartmentModel model)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Department_Update", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                cmd.Parameters.AddWithValue("@DepartmentName", model.DepartmentName);
                cmd.Parameters.AddWithValue("@OrganizationID", model.OrganizationID);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        public bool Delete(int DepartmentID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Department_Delete", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@DepartmentID", DepartmentID);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public DepartmentModel SelectByPK(int DepartmentID)
        {
            DepartmentModel model = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("PR_Department_SelectByPK", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                };
                command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    model = new DepartmentModel
                    {
                        DepartmentID = Convert.ToInt32(reader["DepartmentID"]),
                        DepartmentName = reader["DepartmentName"].ToString(),
                        OrganizationID = Convert.ToInt32(reader["OrganizationID"]),
                    };
                }
            }
            return model;
        }
    }
}
