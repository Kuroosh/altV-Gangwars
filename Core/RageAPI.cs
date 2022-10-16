using AltV.Net;
using AltV.Net.Elements.Entities;
using AltV.Net.Resources.Chat.Api;
using Gangwars.Model;
using System;
using System.Drawing;
using System.Numerics;

namespace Gangwars.Core
{
    public static class RageAPI
    {
        public static void DrawNotification(this PlayerModel element, string errortype, string msg)
        {
            try
            {
                element.Emit("createVnXLiteNotify", errortype, msg);
            }
            catch (Exception ex) { Debug.CatchExceptions("DrawNotification", ex); }
        }
        public static void SpawnPlayer(this PlayerModel element, Vector3 pos, uint DelayInMS = 0)
        {
            try
            {
                if (element.vnxGetElementData<bool>("RAGEAPI:SpawnedPlayer") != true)
                {
                    element.vnxSetElementData("RAGEAPI:SpawnedPlayer", true);
                    element.position = pos;
                    element.Spawn(pos, DelayInMS);
                    element.Emit("Player:Spawn");
                }
                else
                {
                    element.position = pos;
                }
            }
            catch (Exception ex) { Debug.CatchExceptions("SpawnPlayer", ex); }
        }
        public static void DespawnPlayer(this PlayerModel element)
        {
            try
            {
                if (element.vnxGetElementData<bool>("RAGEAPI:SpawnedPlayer") == true)
                {
                    element.vnxSetStreamSharedElementData("RAGEAPI:SpawnedPlayer", false);
                    element.Despawn();
                }
            }
            catch { }
        }
        public static void SetPlayerSkin(this PlayerModel element, uint SkinHash)
        {
            try
            {
                if (element.vnxGetElementData<bool>("RAGEAPI:SpawnedPlayer") == true)
                {
                    if (element.vnxGetElementData<uint>("RAGEAPI:PlayerSkin") != SkinHash)
                    {
                        element.vnxSetStreamSharedElementData("RAGEAPI:PlayerSkin", SkinHash);
                        element.Model = SkinHash;
                    }
                }
            }
            catch { }
        }
        public static uint GetPlayerSkin(this PlayerModel element)
        {
            try
            {
                if (element.vnxGetElementData<bool>("RAGEAPI:SpawnedPlayer") == true)
                {
                    return element.Model;
                }
                return (uint)AltV.Net.Enums.PedModel.Natalia;
            }
            catch { return (uint)AltV.Net.Enums.PedModel.Natalia; }
        }
        public static T vnxGetElementData<T>(this IBaseObject element, string key)
        {
            try
            {
                if (element.GetData(key, out T value)) { return value; }
                return default;
            }
            catch { return default; }
        }
        public static void vnxSetElementData(this IBaseObject element, string key, object value)
        {
            try { element.SetData(key, value); }
            catch (Exception ex) { Debug.CatchExceptions("vnxSetElementData", ex); }
        }
        public static void vnxSetSharedElementData<T>(this IEntity element, string key, T value)
        {
            try
            {
                element.SetData(key, value);
                element.SetSyncedMetaData(key, value);
            }
            catch (Exception ex) { Debug.CatchExceptions("vnxSetSharedElementData", ex); }
        }
        public static void vnxSetStreamSharedElementData<T>(this IEntity element, string key, T value)
        {
            try
            {
                element.SetData(key, value);
                element.SetStreamSyncedMetaData(key, value);
            }
            catch (Exception ex) { Debug.CatchExceptions("vnxSetStreamSharedElementData", ex); }
        }
        public static void Repair(this IVehicle element)
        {
            try
            {
                foreach (PlayerModel player in Alt.GetAllPlayers()) { player.Emit("Vehicle:Repair", element); }
            }
            catch (Exception ex) { Debug.CatchExceptions("Repair", ex); }
        }
        public static T vnxGetSharedData<T>(this IEntity element, string key)
        {
            try
            {
                if (element.GetSyncedMetaData(key, out T value))
                {
                    return value;
                }
                return default;
            }
            catch { return default; }
        }
        public static string GetHexColorcode(int r, int g, int b)
        {
            try
            {
                Color myColor = Color.FromArgb(r, g, b);
                return "{" + myColor.R.ToString("X2") + myColor.G.ToString("X2") + myColor.B.ToString("X2") + "}";
            }
            catch { return ""; }
        }
        public static void WarpIntoVehicle(this PlayerModel player, IVehicle veh, int seat)
        {
            try
            {
                player.Emit("Player:WarpIntoVehicle", veh, seat);
            }
            catch { }
        }
        public static void WarpOutOfVehicle(this PlayerModel player)
        {
            try
            {
                player.Emit("Player:WarpOutOfVehicle");
            }
            catch { }
        }
        public static void Freeze(this PlayerModel player, bool frozen)
        {
            try
            {
                player.Emit("Player:Freeze", frozen);
            }
            catch { }
        }
        public static void Team(this PlayerModel player, int TeamId)
        {
            try
            {
                Alt.Emit("GlobalSystems:PlayerTeam", player, TeamId);
            }
            catch { }
        }
        public static void SetVnXName(this PlayerModel player, string Name)
        {
            try
            {
                player.vnxSetElementData(Globals.EntityData.PLAYER_NAME, Name);
                player.SetStreamSyncedMetaData(Globals.EntityData.PLAYER_NAME, Name);
            }
            catch { }
        }
        public static string GetVnXName(this PlayerModel player)
        {
            try
            {
                return player.vnxGetElementData<string>(Globals.EntityData.PLAYER_NAME);
            }
            catch { return "ERROR"; }
        }
        public static PlayerModel GetPlayerFromName(string name)
        {
            PlayerModel player = null;
            try
            {
                name = name.ToLower();
                foreach (PlayerModel players in Alt.GetAllPlayers())
                {
                    if (players.GetVnXName().ToLower() == name)
                    {
                        player = players;
                    }
                }
                return player;
            }
            catch { return player; }
        }
        public static void GivePlayerWeapon(this PlayerModel player, AltV.Net.Enums.WeaponModel weapon, int ammo)
        {
            try
            {
                Alt.Emit("GlobalSystems:GiveWeapon", player, (uint)weapon, ammo, false);
                //player.GiveWeapon(weapon, ammo, false);
            }
            catch { }
        }
        public static void RemovePlayerWeapon(this PlayerModel player, AltV.Net.Enums.WeaponModel weapon)
        {
            try
            {
                Alt.Emit("GlobalSystems:RemovePlayerWeapon", player, (uint)weapon);
            }
            catch { }
        }
        public static void RemoveAllPlayerWeapons(this PlayerModel player)
        {
            try
            {
                Alt.Emit("GlobalSystems:RemoveAllPlayerWeapons", player);
                //player.GiveWeapon(weapon, ammo, false);
            }
            catch { }
        }
        public static void SendChatMessageToAll(string text)
        {
            try
            {
                foreach (PlayerModel players in Alt.GetAllPlayers())
                {
                    players.SendChatMessage(text);
                }
            }
            catch { }
        }
        public static void SetClothes(this PlayerModel element, int clothesslot, int clothesdrawable, int clothestexture, int clothespalette)
        {
            if (clothesslot < 0 || clothesdrawable < 0) { return; }
            try { element.Emit("Clothes:Load", clothesslot, clothesdrawable, clothestexture, clothespalette); }
            catch (Exception ex) { Debug.CatchExceptions("SetClothes", ex); }
        }
        public static void SetProp(this PlayerModel element, int propID, int drawableID, int textureID)
        {
            if (propID < 0 || textureID < 0) { return; }
            try { element.Emit("Prop:Load", propID, drawableID, textureID); }
            catch (Exception ex) { Debug.CatchExceptions("SetProp", ex); }
        }
        public static void SetAccessories(IPlayer element, int clothesslot, int clothesdrawable, int clothestexture)
        {
            try { element.Emit("Accessories:Load", clothesslot, clothesdrawable, clothestexture); }
            catch { }
        }
        public static void SetPlayerVisible(this PlayerModel element, bool trueOrFalse)
        {
            try { element.Emit("Player:Visible", trueOrFalse); }
            catch { }
        }
        public static void SetPlayerAlpha(this PlayerModel element, int alpha)
        {
            try { element.Emit("Player:Alpha", alpha); }
            catch { }
        }
        public static float ToRadians(float val)
        {
            try
            {
                return (float)(Math.PI / 180) * val;
            }
            catch { return 0; }
        }
        public static float ToDegrees(float val)
        {
            try
            {
                return (float)(val * (180 / Math.PI));
            }
            catch { return 0; }
        }
    }
}
