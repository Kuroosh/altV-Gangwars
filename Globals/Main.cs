using AltV.Net;
using Gangwars.Core;
using Gangwars.Model;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Gangwars.Globals
{
    public class Main
    {
        public static List<RegisterModel> RegisteredAccounts;
        public static List<Banmodel> BannedAccounts;
        public static Timer ScoreboardTimer = new Timer(Scoreboard.Main.OnScoreboardUpdate, 0, 7000, 7000);
        public static Timer OnMinuteSpent = new Timer(OnMinuteSpend, 0, 60000, 60000);
        public static DateTime CurrentIngameTime = DateTime.Now;
        public static int CurrentWeather = 0;

        public static void OnResourceStart()
        {
            Database.Main.OnResourceStart();
        }

        public static void OnMinuteSpend(Object unused)
        {
            try
            {
                foreach (PlayerModel playerClass in Alt.GetAllPlayers())
                {
                    if (playerClass._LoggedIn)
                    {
                        CurrentIngameTime = DateTime.Now.AddHours(1);
                        playerClass.SetDateTime(DateTime.Now.Day, DateTime.Now.Day, DateTime.Now.Day, CurrentIngameTime.Hour, CurrentIngameTime.Minute, CurrentIngameTime.Second);
                        //playerClass.SetWeather((uint)CurrentWeather);
                        playerClass._Playtime += 1;
                        Database.Main.SaveCharacterInformation(playerClass);
                        Core.Debug.OutputLog("Character Information for [" + playerClass.GetVnXName() + "] saved...", ConsoleColor.Green);
                    }
                }
            }
            catch { }
        }

        public static PlayerModel FindPlayerByName(string Name)
        {
            foreach (PlayerModel player in Alt.GetAllPlayers())
            {
                if (player.GetVnXName().ToLower() == Name.ToLower())
                {
                    return player;
                }
            }
            return null;
        }
    }
}
