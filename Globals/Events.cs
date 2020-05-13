using AltV.Net;
using AltV.Net.Elements.Entities;
using Gangwars.Core;
using Gangwars.Model;
using System;

namespace Gangwars.Globals
{
    public class Events : IScript
    {
        [ServerEvent("GlobalSystems:PlayerReady")]
        public void OnPlayerConnect(PlayerModel playerClass)
        {
            playerClass.OnPlayerConnect();
            RegisterLogin.Main.LoadGangAreas(playerClass);
            RegisterLogin.Main.LoadGangVehicleSpawnpoints(playerClass);
        }

        [ScriptEvent(ScriptEventType.PlayerDamage)]
        public static void PlayerDamage(PlayerModel source, IEntity entity, uint weapon, ushort damage)
        {
            try
            {
                source?.Emit("Globals:ShowBloodScreen");
            }
            catch { }
        }
        [ScriptEvent(ScriptEventType.PlayerDisconnect)]
        public void OnPlayerDisconnect(PlayerModel client, string reason)
        {
            try
            {
                Database.Main.SaveCharacterInformation(client);
                client?.RemoveAllPlayerWeapons();
            }
            catch (Exception ex) { Core.Debug.CatchExceptions("OnPlayerDisconnect", ex); }
        }

        /*
        [ServerEvent("GlobalSystems:OnPlayerSyncDeath")]
        public void OnPlayerSyncDeath(PlayerModel player, PlayerModel killer)
        {
            try
            {
                if (killer == player)
                {
                    player.SendChatMessage("you killed yourself!");
                }
                else
                {
                    player.SendChatMessage(killer.GetVnXName() + " killed you!");
                    killer._Kills += 1;
                }
            }
            catch { }
        }*/

        [ServerEvent("GlobalSystems:OnPlayerSyncDamage")]
        public void OnPlayerSyncDamage(PlayerModel player, PlayerModel killer)
        {
            try
            {
                player?.Emit("Globals:ShowBloodScreen");
                killer?.Emit("Globals:PlayHitsound");
                player?.vnxSetElementData("Gangwar:LastDamaged", killer);
            }
            catch { }
        }


        [ScriptEvent(ScriptEventType.PlayerDead)]
        public static void OnPlayerDeath(PlayerModel player, IEntity entity, uint reason)
        {
            try
            {
                if (entity is IVehicle vehicle)
                {
                    Debug.SendChatMessageToAll(player.GetVnXName() + " got killed by a Vehicle...");
                }
                else if (entity is PlayerModel killergot)
                {
                    Debug.SendChatMessageToAll(player.GetVnXName() + " got killed by " + RageAPI.GetHexColorcode(200, 0, 0) + killergot.GetVnXName());
                    killergot._Kills += 1;
                    killergot._EXP += 2;
                    if (killergot._EXP >= 100)
                    {
                        killergot._EXP = 0;
                        killergot._Level += 1;
                        Debug.SendChatMessageToAll(RageAPI.GetHexColorcode(0, 200, 255) + killergot.GetVnXName() + " is now Level " + killergot._Level + "!");
                    }
                }
                else
                {
                    PlayerModel killer = player.vnxGetElementData<PlayerModel>("Gangwar:LastDamaged");
                    if (killer != null)
                    {
                        Debug.SendChatMessageToAll(player.GetVnXName() + " got killed by " + RageAPI.GetHexColorcode(200, 0, 0) + killer.GetVnXName());
                        killer._Kills += 1;
                        killer._EXP += 2;
                        if (killer._EXP >= 100)
                        {
                            killer._EXP = 0;
                            killer._Level += 1;
                            Debug.SendChatMessageToAll(RageAPI.GetHexColorcode(0, 200, 255) + killer.GetVnXName() + " is now Level " + killer._Level + "!");
                        }
                    }
                }
                player._Deaths += 1;
                RegisterLogin.Main.SpawnPlayerByTeamId(player, player.vnxGetElementData<int>(EntityData.PLAYER_TEAM));
            }
            catch { }
        }
    }
    public static class EventFunctions
    {
        public static void OnPlayerConnect(this PlayerModel playerClass)
        {
            try { RegisterLogin.Main.ShowRegisterLogin(playerClass); }
            catch (Exception ex) { Core.Debug.CatchExceptions("OnPlayerConnect", ex); }
        }
    }
}
