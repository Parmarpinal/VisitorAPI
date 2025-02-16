using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting;
using System;
using System.Data;
using VisitorWebAPI.Models;

namespace VisitorWebAPI.Data
{
    public class DashboardRepository
    {
        private readonly string _connectionString;
        public DashboardRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }
        public DashBoardModel GetData()
        {
            var dashboardData = new DashBoardModel
            {
                Counts = new List<DashboardCounts>(),
                RecentOrganizations = new List<RecentOrganizationModel>(),
                RecentDepartments = new List<RecentDepartmentModel>(),
                TopReceptionists = new List<TopReceptionistModel>(),
                TopVisitors = new List<TopVisitorModel>(),
                TopHosts = new List<TopHostModel>(),
            };

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("usp_GetAdminDashboardData", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            // Fetch counts
                            while (reader.Read())
                            {
                                dashboardData.Counts.Add(new DashboardCounts
                                {
                                    Metric = reader["Metric"].ToString(),
                                    Value = Convert.ToInt32(reader["Value"])
                                });
                            }

                            // Fetch recent organizations
                            if (reader.NextResult())
                            {
                                while (reader.Read())
                                {
                                    dashboardData.RecentOrganizations.Add(new RecentOrganizationModel
                                    {
                                        OrganizationID = Convert.ToInt32(reader["OrganizationID"]),
                                        OrganizationName = reader["OrganizationName"].ToString(),
                                        DepartmentCount = Convert.ToInt32(reader["DepartmentCount"]),
                                        ReceptionistCount = Convert.ToInt32(reader["ReceptionistCount"]),
                                        Head = reader["Head"].ToString(),
                                        City = reader["City"].ToString(),
                                        EstablishedDate = Convert.ToDateTime(reader["EstablishedDate"])
                                    });
                                }
                            }

                            // Fetch recent departments
                            if (reader.NextResult())
                            {
                                while (reader.Read())
                                {
                                    dashboardData.RecentDepartments.Add(new RecentDepartmentModel
                                    {
                                        DepartmentID = Convert.ToInt32(reader["DepartmentID"]),
                                        DepartmentName = reader["DepartmentName"].ToString(),
                                        OrganizationID = Convert.ToInt32(reader["OrganizationID"]),
                                        OrganizationName = reader["OrganizationName"].ToString(),
                                        HostCount = Convert.ToInt32(reader["HostCount"])
                                    });
                                }
                            }

                            // Fetch top receptionist
                            if ( reader.NextResult())
                            {
                                while ( reader.Read())
                                {
                                    dashboardData.TopReceptionists.Add(new TopReceptionistModel
                                    {
                                        ReceptionistID = Convert.ToInt32(reader["ReceptionistID"]),
                                        ReceptionistName = reader["ReceptionistName"].ToString(),
                                        OrganizationID = Convert.ToInt32(reader["OrganizationID"]),
                                        OrganizationName = reader["OrganizationName"].ToString(),
                                        JoiningDate = Convert.ToDateTime(reader["JoiningDate"])
                                    });
                                }
                            }

                            // Fetch top hosts
                            if (reader.NextResult())
                            {
                                while (reader.Read())
                                {
                                    dashboardData.TopHosts.Add(new TopHostModel
                                    {
                                        HostID = Convert.ToInt32(reader["HostID"]),
                                        HostName = reader["HostName"].ToString(),
                                        OrganizationID = Convert.ToInt32(reader["OrganizationID"]),
                                        DepartmentID = Convert.ToInt32(reader["DepartmentID"]),
                                        OrganizationName = reader["OrganizationName"].ToString(),
                                        DepartmentName = reader["DepartmentName"].ToString(),
                                        VisitorCount = Convert.ToInt32(reader["VisitorCount"]),
                                        JoiningDate = Convert.ToDateTime(reader["JoiningDate"])
                                    });
                                }
                            }

                            // Fetch top visitors
                            if ( reader.NextResult())
                            {
                                while ( reader.Read())
                                {
                                    dashboardData.TopVisitors.Add(new TopVisitorModel
                                    {
                                        VisitorID = Convert.ToInt32(reader["VisitorID"]),
                                        UserID = Convert.ToInt32(reader["UserID"]),
                                        UserName = reader["UserName"].ToString(),
                                        Purpose = reader["Purpose"].ToString(),
                                        HostID = Convert.ToInt32(reader["HostID"]),
                                        HostName = reader["HostName"].ToString(),
                                        VisitDate = Convert.ToDateTime(reader["VisitDate"]),
                                        CheckIn = reader["CheckIn"] != DBNull.Value && !string.IsNullOrWhiteSpace(reader["CheckIn"].ToString())
                                                    ? TimeOnly.Parse(reader["CheckIn"].ToString())
                                                    : null,
                                        CheckOut = reader["CheckOut"] != DBNull.Value && !string.IsNullOrWhiteSpace(reader["CheckOut"].ToString())
                                                    ? TimeOnly.Parse(reader["CheckOut"].ToString())
                                                    : null,
                                        OrganizationID = Convert.ToInt32(reader["OrganizationID"]),
                                        DepartmentID = Convert.ToInt32(reader["DepartmentID"]),
                                        OrganizationName = reader["OrganizationName"].ToString(),
                                        DepartmentName = reader["DepartmentName"].ToString(),
                                    });
                                }
                            }
                           
                        }
                    }
                }
            }

            //var model = new Dashboard
            //{
            //    Counts = dashboardData.Counts,
            //    RecentOrgs = dashboardData.RecentOrgs,
            //    RecentDepts = dashboardData.RecentDepts,
            //    TopRecs = dashboardData.TopRecs,
            //    TopVts = dashboardData.TopVts,
            //    TopHsts = dashboardData.TopHsts,
            //};

            return dashboardData;
        }
    }
}
