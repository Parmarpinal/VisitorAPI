namespace VisitorWebAPI.Models
{
    public class DepartmentModel
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public int OrganizationID { get; set; }
        public string? OrganizationName { get; set; }

        public int? HostCount { get; set; }
    }
}
