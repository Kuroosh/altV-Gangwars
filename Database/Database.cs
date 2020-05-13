using AltV.Net.Async;
using Gangwars.Core;
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
                    Globals.Main.BannedAccounts = LoadBannedAccountlist();
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
        private static List<Banmodel> LoadBannedAccountlist()
        {
            try
            {
                List<Banmodel> BanList = new List<Banmodel>();
                using MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM bans";
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Banmodel BanModelClass = new Banmodel(reader);
                    BanList.Add(BanModelClass);
                }
                return BanList;
            }
            catch (Exception ex) { Core.Debug.CatchExceptions("LoadRegisteredAccountlist", ex); return new List<Banmodel>(); }
        }

        public static void ChangeUserPasswort(string Name, string Password)
        {
            try
            {
                using MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE player SET Password = SHA2(@Password, '256') WHERE Name = @Name";
                command.Parameters.AddWithValue("@Name", Name);
                command.Parameters.AddWithValue("@Password", Password);
                command.ExecuteNonQuery();
            }
            catch { }
        }
        public static void LoadCharacterInformation(PlayerModel character)
        {
            try
            {
                using MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM user WHERE Name = @Name LIMIT 1";
                command.Parameters.AddWithValue("@Name", character.GetVnXName());

                using MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();

                    character._UID = reader.GetInt32("UID");
                    character._Alevel = reader.GetInt32("ALevel");
                    character._Playtime = reader.GetInt32("Playtime");
                    character._Kills = reader.GetInt32("Kills");
                    character._Deaths = reader.GetInt32("Deaths");
                    character._MaxStreaks = reader.GetInt32("MaxStreak");
                    character._Cstreak = reader.GetInt32("CStreak");
                    character._Level = reader.GetInt32("Level");
                    character._EXP = reader.GetInt32("EXP");

                }
            }
            catch (Exception ex) { Debug.CatchExceptions("LoadCharacterInformationById", ex); }
        }
        public static void AddPlayerBan(PlayerModel Admin, PlayerModel target, DateTime BanTill, string reason, string BanType)
        {
            using MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();

                command.CommandText = "INSERT INTO bans (UID, Name, HardwareIdHash, HardwareIdExHash, SocialID, Reason, Admin, BanTime, BanCreated, BanType) VALUES (@UID, @Name, @HardwareIdHash, @HardwareIdExHash, @SocialID, @Reason, @Admin, @BanTime, @BanCreated, @BanType)";
                command.Parameters.AddWithValue("@UID", target._UID);
                command.Parameters.AddWithValue("@Name", target.GetVnXName());
                command.Parameters.AddWithValue("@HardwareIdHash", target.HardwareIdHash);
                command.Parameters.AddWithValue("@HardwareIdExHash", target.HardwareIdExHash);
                command.Parameters.AddWithValue("@SocialID", target.SocialClubId);
                command.Parameters.AddWithValue("@Reason", reason);
                command.Parameters.AddWithValue("@Admin", Admin.GetVnXName());
                command.Parameters.AddWithValue("@BanTime", BanTill);
                command.Parameters.AddWithValue("@BanCreated", DateTime.Now);
                command.Parameters.AddWithValue("@BanType", BanType);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("[EXCEPTION AddPlayerTimeBan] " + ex.Message);
                Console.WriteLine("[EXCEPTION AddPlayerTimeBan] " + ex.StackTrace);
            }
        }

        public static async void SaveCharacterInformation(PlayerModel player)
        {
            try
            {
                await Task.Run(async () =>
                {
                    await AltAsync.Do(() =>
                    {
                        using MySqlConnection connection = new MySqlConnection(connectionString);
                        connection.Open();
                        MySqlCommand command = connection.CreateCommand();
                        command.CommandText = "UPDATE user SET Playtime = @Playtime, Kills = @Kills, Deaths = @Deaths, MaxStreak = @MaxStreak, CStreak = @CStreak, Level = @Level, EXP = @EXP WHERE Name = @Name";
                        command.Parameters.AddWithValue("@Name", player.GetVnXName());
                        command.Parameters.AddWithValue("@Playtime", player._Playtime);
                        command.Parameters.AddWithValue("@Kills", player._Kills);
                        command.Parameters.AddWithValue("@Deaths", player._Deaths);
                        command.Parameters.AddWithValue("@MaxStreak", player._MaxStreaks);
                        command.Parameters.AddWithValue("@CStreak", player._Cstreak);
                        command.Parameters.AddWithValue("@Level", player._Level);
                        command.Parameters.AddWithValue("@EXP", player._EXP);
                        command.ExecuteNonQuery();
                    });
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("[EXCEPTION SaveCharacterInformation] " + ex.Message);
                Console.WriteLine("[EXCEPTION SaveCharacterInformation] " + ex.StackTrace);
            }
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
            try
            {
                using MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO user (Name) VALUES(@Name)";
                command.Parameters.AddWithValue("@Name", username);
                command.ExecuteNonQuery();
            }
            catch (Exception ex) { Core.Debug.CatchExceptions("RegisterAccountSecondStep", ex); }
        }
    }
}
