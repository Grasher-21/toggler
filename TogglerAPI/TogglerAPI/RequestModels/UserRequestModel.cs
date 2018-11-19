namespace TogglerAPI.RequestModels
{
    public class UserRequestModel
    {
        public int? UserId { get; set; }
        public int RoleId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
