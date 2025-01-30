namespace VisitorWebAPI.Models
{
    public class UserModel
    {
        public int UserID { get; set; }
        public int UserTypeID { get; set; }
        public string? UserTypeName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
        public int? OrganizationID { get; set; }
        public string? OrganizationName { get; set; }
        public int? DepartmentID { get; set; }
        public string? DepartmentName { get; set; }
        public DateTime? JoiningDate { get; set; }
    }

}
