namespace ZPZP.Models
{
    public class Users
    {
        public int ID { get; set; }
        public string? UserName { get; set; }
        public string? UserPassword { get; set; }
        public int UserDomain { get; set; }
        public string? UserCategory { get; set; }
        public string? UserLevel { get; set; }
        public string? Name { get; set; }   
        public string? Surname { get; set; }    
        public string? Email { get; set; }
        public int PhoneNumber { get; set; }
        public byte[]? Image { get; set; }
    }
}
