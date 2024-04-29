namespace WebApplication5.DTO
{
    public class UserRoleDto
    {
        public Guid IDUser { get; set; }
        public string NameUser { get; set; }
        public string SurnameUser { get; set; }
        public string EmailUser { get; set; }

        public Guid TeamId { get; set; }
        public string NameRole { get; set; }
    }
}
