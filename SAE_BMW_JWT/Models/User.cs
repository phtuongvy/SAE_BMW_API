namespace SAE_BMW_JWT.Models
{
    public class User
    {
        public string UserName { get; set; }
        public string? NomClient { get; set; }
        public string? PrenomClient { get; set; }
        public byte[]? Password { get; set; }
        public string? ClientRole { get; set; }
    }
}
