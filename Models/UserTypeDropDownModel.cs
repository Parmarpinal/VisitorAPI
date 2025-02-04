namespace VisitorWebAPI.Models
{
    public class UserTypeDropDownModel
    {
        public int UserTypeID { get; set; }
        public string UserTypeName { get; set; }
    }
    public class OrganizationDropDownModel
    {
        public int OrganizationID { get; set; }
        public string OrganizationName { get; set; }
    }
    public class DepartmentDropDownModel
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
    }
    public class UserDropDownModel
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
    }
    public class HostDropDownModel
    {
        public int HostID { get; set; }
        public string HostName { get; set; }
    }
}
