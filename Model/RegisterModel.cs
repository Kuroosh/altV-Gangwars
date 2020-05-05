using MySql.Data.MySqlClient;

namespace Gangwars.Model
{
    public class RegisterModel
    {
        public string UID { get; set; }
        public string Username { get; set; }
        public string HardwareId { get; set; }
        public string HardwareIdExHash { get; set; }
        public string Password { get; set; }
        public string SocialID { get; set; }

        public RegisterModel(MySqlDataReader reader)
        {
            UID = reader.GetString("UID");
            Username = reader.GetString("Name");
            HardwareId = reader.GetString("HardwareIdHash");
            HardwareIdExHash = reader.GetString("HardwareIdExHash");
            Password = reader.GetString("Password");
            SocialID = reader.GetString("SocialID");
        }
        public RegisterModel() { }
    }
}

