using Microsoft.Data.SqlClient;
using VisitorWebAPI.Models;

namespace VisitorWebAPI.Data
{
    public class UserTypeRepository
    {
        private readonly string _connectionString;
        public UserTypeRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }
        public IEnumerable<UserTypeModel> SelectAll()
        {
            var users = new List<UserTypeModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_UserType_SelectAll", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    users.Add(new UserTypeModel
                    {
                        UserTypeID = Convert.ToInt32(reader["UserTypeID"]),
                        UserTypeName = reader["UserTypeName"].ToString()
                    });
                }
            }
            return users;
        }
        public bool Add(UserTypeModel model)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_UserType_Insert", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@UserTypeName", model.UserTypeName);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        public bool Edit(UserTypeModel model)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_UserType_Update", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@UserTypeID", model.UserTypeID);
                cmd.Parameters.AddWithValue("@UserTypeName", model.UserTypeName);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        public bool Delete(int UserTypeID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_UserType_Delete", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@UserTypeID", UserTypeID);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        public UserTypeModel SelectByPK(int usertypeID)
        {
            UserTypeModel model = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("PR_UserType_SelectByPK", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                };
                command.Parameters.AddWithValue("@UserTypeID", usertypeID);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    model = new UserTypeModel
                    {
                        UserTypeID = Convert.ToInt32(reader["UserTypeID"]),
                        UserTypeName = reader["UserTypeName"].ToString(),
                    };
                }
            }
            return model;
        }

    }
}
