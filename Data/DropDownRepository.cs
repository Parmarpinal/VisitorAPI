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
    }
}
