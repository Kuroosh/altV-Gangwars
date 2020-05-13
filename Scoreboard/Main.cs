using AltV.Net;
using Gangwars.Core;
using Gangwars.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Gangwars.Scoreboard
{
    public class Main
    {
        public static void OnScoreboardUpdate(Object unused)
        {
            List<ScoreboardModel> AllPlayer = new List<ScoreboardModel>();
            foreach (PlayerModel player in Alt.GetAllPlayers())
            {
                TimeSpan PlayTimeSpan = TimeSpan.FromMinutes(player._Playtime);
                string NormalizedPlaytime = string.Format("{0:00}:{1:00}", (int)PlayTimeSpan.TotalHours, PlayTimeSpan.Minutes);
                if (player._LoggedIn)
                {
                    ScoreboardModel cplayerModel = new ScoreboardModel
                    {
                        Name = player.GetVnXName(),
                        Kills = player._Kills.ToString(),
                        Tode = player._Deaths.ToString(),
                        Playtime = NormalizedPlaytime,
                        Ping = player.Ping.ToString(),
                        Streaks = player._Level.ToString()
                    };
                    AllPlayer.Add(cplayerModel);
                }
                else
                {
                    ScoreboardModel cplayerModel = new ScoreboardModel
                    {
                        Name = player.Name,
                        Kills = "-",
                        Tode = "-",
                        Playtime = "Connecting...",
                        Ping = "-",
                        Streaks = "-"
                    };
                    AllPlayer.Add(cplayerModel);
                }
            }
            foreach (PlayerModel player in Alt.GetAllPlayers())
            {
                player.Emit("Scoreboard:Update", JsonConvert.SerializeObject(AllPlayer));
            }
        }
    }
}
