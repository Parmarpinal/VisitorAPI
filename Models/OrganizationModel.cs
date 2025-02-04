namespace VisitorWebAPI.Models
{
    public class OrganizationModel
    {
        public int OrganizationID { get; set; }
        public string OrganizationName { get; set; }
        public string Head { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public DateTime EstablishedDate { get; set; }

        public int? ReceptionistCount { get; set; }
    }
}
