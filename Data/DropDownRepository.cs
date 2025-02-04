using Microsoft.Data.SqlClient;
using VisitorWebAPI.Models;

namespace VisitorWebAPI.Data
{
    public class DropDownRepository
    {
        private readonly string _connectionString;
        public DropDownRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }
        public IEnumerable<UserTypeDropDownModel> SelectAllUserType()
        {
            var users = new List<UserTypeDropDownModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_UserType_SelectForDropDown", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    users.Add(new UserTypeDropDownModel
                    {
                        UserTypeID = Convert.ToInt32(reader["UserTypeID"]),
                        UserTypeName = reader["UserTypeName"].ToString(),
                    });
                }
            }
            return users;
        }
        public IEnumerable<OrganizationDropDownModel> SelectAllOrganization()
        {
            var organizations = new List<OrganizationDropDownModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Organization_SelectForDropDown", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    organizations.Add(new OrganizationDropDownModel
                    {
                        OrganizationID = Convert.ToInt32(reader["OrganizationID"]),
                        OrganizationName = reader["OrganizationName"].ToString(),
                    });
                }
            }
            return organizations;
        }
        public IEnumerable<DepartmentDropDownModel> SelectAllDepartment(int organizationID)
        {
            var department = new List<DepartmentDropDownModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Department_SelectForDropDown", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@OrganizationID", organizationID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    department.Add(new DepartmentDropDownModel
                    {
                        DepartmentID = Convert.ToInt32(reader["DepartmentID"]),
                        DepartmentName = reader["DepartmentName"].ToString(),
                    });
                }
            }
            return department;
        }

        public IEnumerable<UserDropDownModel> SelectAllUser()
        {
            var users = new List<UserDropDownModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("Select UserID, UserName from [User] where UserTypeID = 3", conn)
                {
                    CommandType = System.Data.CommandType.Text
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    users.Add(new UserDropDownModel
                    {
                        UserID = Convert.ToInt32(reader["UserID"]),
                        UserName = reader["UserName"].ToString(),
                    });
                }
            }
            return users;
        }

        public IEnumerable<HostDropDownModel> SelectAllHost(int departmentID)
        {
            var users = new List<HostDropDownModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand($"Select UserID, UserName from [User] where UserTypeID = 1 and DepartmentID = {departmentID}", conn)
                {
                    CommandType = System.Data.CommandType.Text
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    users.Add(new HostDropDownModel
                    {
                        HostID = Convert.ToInt32(reader["UserID"]),
                        HostName = reader["UserName"].ToString(),
                    });
                }
            }
            return users;
        }
    }
}
