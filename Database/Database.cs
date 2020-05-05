using AltV.Net.Async;
using Gangwars.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gangwars.Database
{
    class Main
    {
        public static string connectionString;

        public static async void OnResourceStart()
        {
            string host = "127.0.0.1";
            string user = "Gangwars";
            string pass = "0M1ae!0u!a71s3vUl9M6nm%3";
            string db = "Gangwars";
            connectionString = "SERVER=" + host + "; DATABASE=" + db + "; UID=" + user + "; PASSWORD=" + pass + "; SSLMODE=none;";

            await Task.Run(async () =>
            {
                await AltAsync.Do(() =>
                {
                    Globals.Main.RegisteredAccounts = LoadRegisteredAccountlist();
                });
            });
            Core.Debug.OutputLog("--- Database Connection = [OK!]", ConsoleColor.Green);
        }

        private static List<RegisterModel> LoadRegisteredAccountlist()
        {
            try
            {
                List<RegisterModel> RegisterList = new List<RegisterModel>();
                using MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM player";

                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    RegisterModel RegisterModelClass = new RegisterModel(reader);
                    RegisterList.Add(RegisterModelClass);
                }
                return RegisterList;
            }
            catch (Exception ex) { Core.Debug.CatchExceptions("LoadRegisteredAccountlist", ex); return new List<RegisterModel>(); }
        }
        public static void RegisterAccount(string username, string SpielerSocial, string password, string HardwareIdHash, string HardwareIdExHash)
        {
            try
            {
                using MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO player (Name, SocialId, Password, HardwareIdHash, HardwareIdExHash) VALUES(@Name, @SocialId, SHA2(@Password, '256'), @HardwareIdHash, @HardwareIdExHash)";
                command.Parameters.AddWithValue("@Name", username);
                command.Parameters.AddWithValue("@SocialId", SpielerSocial);
                command.Parameters.AddWithValue("@HardwareIdHash", HardwareIdHash);
                command.Parameters.AddWithValue("@HardwareIdExHash", HardwareIdExHash);
                command.Parameters.AddWithValue("@Password", password);
                command.ExecuteNonQuery();
            }
            catch (Exception ex) { Core.Debug.CatchExceptions("RegisterAccount", ex); }
        }
    }
}
