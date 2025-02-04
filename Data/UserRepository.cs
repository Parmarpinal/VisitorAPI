using Microsoft.Data.SqlClient;
using VisitorWebAPI.Models;

namespace VisitorWebAPI.Data
{
    public class UserRepository
    {
        private readonly string _connectionString;
        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }
        public IEnumerable<UserModel> SelectAll(int? organizationID, int? departmentID, int? userTypeID)
        {
            var users = new List<UserModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_User_SelectAll", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@OrganizationID", organizationID);
                cmd.Parameters.AddWithValue("@DepartmentID", departmentID);
                cmd.Parameters.AddWithValue("@UserTypeID", userTypeID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    users.Add(new UserModel
                    {
                        UserID = Convert.ToInt32(reader["UserID"]),
                        UserName = reader["UserName"].ToString(),
                        Email = reader["Email"].ToString(),
                        MobileNo = reader["MobileNo"].ToString(),
                        Gender = reader["Gender"].ToString(),
                        Age = Convert.ToInt32(reader["Age"]),
                        City = reader["City"].ToString(),
                        OrganizationID = reader.IsDBNull(reader.GetOrdinal("OrganizationID")) ? null : Convert.ToInt32(reader["OrganizationID"]),
                        OrganizationName = reader.IsDBNull(reader.GetOrdinal("OrganizationName")) ? null : reader["OrganizationName"].ToString(),
                        DepartmentID = reader.IsDBNull(reader.GetOrdinal("DepartmentID")) ? null : Convert.ToInt32(reader["DepartmentID"]),
                        DepartmentName = reader.IsDBNull(reader.GetOrdinal("DepartmentName")) ? null : reader["DepartmentName"].ToString(),
                        UserTypeID = Convert.ToInt32(reader["UserTypeID"]),
                        UserTypeName = reader["UserTypeName"].ToString(),
                        JoiningDate = Convert.ToDateTime(reader["JoiningDate"]),
                    });
                }
            }
            return users;
        }

        public bool Add(UserModel model)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_User_Insert", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@UserTypeID", model.UserTypeID);
                cmd.Parameters.AddWithValue("@UserName", model.UserName);
                cmd.Parameters.AddWithValue("@Email", model.Email);
                cmd.Parameters.AddWithValue("@MobileNo", model.MobileNo);
                cmd.Parameters.AddWithValue("@Age", model.Age);
                cmd.Parameters.AddWithValue("@Gender", model.Gender);
                cmd.Parameters.AddWithValue("@City", model.City);
                cmd.Parameters.AddWithValue("@OrganizationID", model.OrganizationID);
                cmd.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        public bool Edit(UserModel model)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_User_Update", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@UserID", model.UserID);
                cmd.Parameters.AddWithValue("@UserTypeID", model.UserTypeID);
                cmd.Parameters.AddWithValue("@UserName", model.UserName);
                cmd.Parameters.AddWithValue("@Email", model.Email);
                cmd.Parameters.AddWithValue("@MobileNo", model.MobileNo);
                cmd.Parameters.AddWithValue("@Age", model.Age);
                cmd.Parameters.AddWithValue("@Gender", model.Gender);
                cmd.Parameters.AddWithValue("@City", model.City);
                cmd.Parameters.AddWithValue("@OrganizationID", model.OrganizationID);
                cmd.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        public bool Delete(int UserID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_User_Delete", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@UserID", UserID);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public UserModel SelectByPK(int userID)
        {
            UserModel model = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("PR_User_SelectByPK", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                };
                command.Parameters.AddWithValue("@UserID", userID);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    model = new UserModel
                    {
                        UserID = Convert.ToInt32(reader["UserID"]),
                        UserName = reader["UserName"].ToString(),
                        Email = reader["Email"].ToString(),
                        MobileNo = reader["MobileNo"].ToString(),
                        Gender = reader["Gender"].ToString(),
                        Age = Convert.ToInt32(reader["Age"]),
                        City = reader["City"].ToString(),
                        OrganizationID = reader.IsDBNull(reader.GetOrdinal("OrganizationID")) ? null : Convert.ToInt32(reader["OrganizationID"]),
                        DepartmentID = reader.IsDBNull(reader.GetOrdinal("DepartmentID")) ? null : Convert.ToInt32(reader["DepartmentID"]),
                        UserTypeID = Convert.ToInt32(reader["UserTypeID"])
                    };
                }
            }
            return model;
        }

        public UserModel SelectSpecificUser(int userID)
        {
            UserModel model = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("PR_User_SelectSpecificType", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                };
                command.Parameters.AddWithValue("@UserID", userID);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    model = new UserModel
                    {
                        UserID = Convert.ToInt32(reader["UserID"]),
                        UserName = reader["UserName"].ToString(),
                        Email = reader["Email"].ToString(),
                        MobileNo = reader["MobileNo"].ToString(),
                        Gender = reader["Gender"].ToString(),
                        Age = Convert.ToInt32(reader["Age"]),
                        City = reader["City"].ToString(),
                        OrganizationID = reader.IsDBNull(reader.GetOrdinal("OrganizationID")) ? null : Convert.ToInt32(reader["OrganizationID"]),
                        DepartmentID = reader.IsDBNull(reader.GetOrdinal("DepartmentID")) ? null : Convert.ToInt32(reader["DepartmentID"]),
                        OrganizationName = reader["OrganizationName"].ToString(),
                        DepartmentName = reader["DepartmentName"].ToString(),
                        JoiningDate = Convert.ToDateTime(reader["JoiningDate"]),
                        UserTypeID = Convert.ToInt32(reader["UserTypeID"])
                    };
                }
            }
            return model;
        }
    }
}
