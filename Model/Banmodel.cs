using MySql.Data.MySqlClient;
using System;

namespace Gangwars.Model
{
    public class Banmodel
    {
        public string UID { get; set; }
        public string Username { get; set; }
        public string HardwareId { get; set; }
        public string HardwareIdExHash { get; set; }
        public string SocialID { get; set; }
        public string Reason { get; set; }
        public string Admin { get; set; }
        public DateTime BanTime { get; set; }
        public DateTime BanCreated { get; set; }
        public string BanType { get; set; }
        public Banmodel(MySqlDataReader reader)
        {
            UID = reader.GetString("UID");
            Username = reader.GetString("Name");
            HardwareId = reader.GetString("HardwareIdHash");
            HardwareIdExHash = reader.GetString("HardwareIdExHash");
            SocialID = reader.GetString("SocialID");
            Reason = reader.GetString("Reason");
            Admin = reader.GetString("Admin");
            BanTime = reader.GetDateTime("BanTime");
            BanCreated = reader.GetDateTime("BanCreated");
            BanType = reader.GetString("BanType");
        }
        public Banmodel() { }
    }
}
