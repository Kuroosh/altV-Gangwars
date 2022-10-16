using AltV.Net;
using AltV.Net.Elements.Entities;
using AltV.Net.Resources.Chat.Api;
using Gangwars.Core;
using Gangwars.Globals;
using Gangwars.Model;
using System;
using System.Numerics;

namespace Gangwars.RegisterLogin
{
    public class Main : IScript
    {
        public static void ShowRegisterLogin(PlayerModel playerClass)
        {
            try
            {
                playerClass.Freeze(true);
                playerClass.Emit("LoginRegister:Create");
            }
            catch { }
        }
        [ClientEvent("Gangwars:Login")]
        public void OnPlayerLoginTry(PlayerModel playerClass, string name, string password)
        {
            try
            {
                foreach (Banmodel banClass in Globals.Main.BannedAccounts)
                {
                    if (banClass.HardwareId == playerClass.HardwareIdHash.ToString() || banClass.HardwareIdExHash == playerClass.HardwareIdExHash.ToString() || banClass.SocialID == playerClass.SocialClubId.ToString())
                    {
                        Debug.OutputLog(playerClass.GetVnXName() + " tried to login... but he is banned :P", ConsoleColor.White);
                        return;
                    }
                }
                string passwordHashed = Debug.Sha256(password);
                foreach (RegisterModel registeredClass in Globals.Main.RegisteredAccounts)
                {
                    if (registeredClass.Username.ToLower() == name.ToLower() && registeredClass.Password == passwordHashed)
                    {
                        playerClass.Emit("LoginRegister:Destroy");
                        playerClass.Emit("TeamSelection:Show");
                        playerClass.SetVnXName(registeredClass.Username);
                        playerClass._LoggedIn = true;
                        Database.Main.LoadCharacterInformation(playerClass);
                    }
                }
            }
            catch (Exception ex) { Debug.CatchExceptions("Gangwars:Login", ex); }
        }
        [ClientEvent("Gangwars:Register")]
        public void OnPlayerRegisterTry(PlayerModel playerClass, string name, string password)
        {
            try
            {
                if (name.Length <= 2 || password.Length <= 2) { return; }
                string passwordHashed = Debug.Sha256(password);
                if (Globals.Main.RegisteredAccounts.Count > 0)
                {
                    foreach (RegisterModel registeredClass in Globals.Main.RegisteredAccounts)
                    {
                        if (registeredClass.Username.ToLower() == name.ToLower())
                        {
                            Debug.OutputDebugString("Der Nutzername exestiert bereits!!");
                            return;
                        }
                        if (registeredClass.HardwareId.ToString() == playerClass.HardwareIdHash.ToString())
                        {
                            Debug.OutputDebugString("Du hast bereits einen Account!");
                            return;
                        }
                    }
                }
                RegisterModel registerClass = new RegisterModel()
                {
                    UID = (Globals.Main.RegisteredAccounts.Count + 1).ToString(),
                    Username = name,
                    Password = passwordHashed,
                    HardwareId = playerClass.HardwareIdHash.ToString(),
                    HardwareIdExHash = playerClass.HardwareIdExHash.ToString()
                };
                Database.Main.RegisterAccount(name, playerClass.SocialClubId.ToString(), password, playerClass.HardwareIdHash.ToString(), playerClass.HardwareIdExHash.ToString());
                Globals.Main.RegisteredAccounts.Add(registerClass);
                playerClass.Emit("LoginRegister:Destroy");
                playerClass.Emit("TeamSelection:Show");
                playerClass.SetVnXName(name);
                playerClass._LoggedIn = true;
            }
            catch (Exception ex) { Debug.CatchExceptions("Gangwars:Register", ex); }
        }

        [ClientEvent("Gangwars:SelectTeam")]
        public void SelectTeam(PlayerModel playerClass, int team)
        {
            try
            {
                playerClass.Team(team);
                playerClass.vnxSetElementData(EntityData.PLAYER_TEAM, team);
                SpawnPlayerByTeamId(playerClass, team);
                playerClass.Freeze(false);
                playerClass.SetPlayerAlpha(255);
                playerClass.SetPlayerVisible(true);
                GivePlayerGangwarWeapons(playerClass);
            }
            catch { }
        }

        [Command("team")]
        public static void SelectTeamCMD(PlayerModel playerClass)
        {
            try
            {
                playerClass.position = new Vector3(0, 0, 0);
                playerClass.Freeze(true);
                playerClass.Emit("TeamSelection:Show");
            }
            catch { }
        }

        public static void SpawnPlayerByTeamId(PlayerModel playerClass, int Id, uint Delay = 0)
        {
            try
            {
                playerClass.vnxSetStreamSharedElementData("RAGEAPI:SpawnedPlayer", false);
                switch (Id)
                {
                    case Constants.FACTION_LSPD:
                        playerClass.SpawnPlayer(Constants.FACTION_LSPD_SPAWN, Delay);
                        LoadGangSkin(playerClass, Id);
                        break;
                    case Constants.FACTION_LCN:
                        playerClass.SpawnPlayer(Constants.FACTION_LCN_SPAWN, Delay);
                        LoadGangSkin(playerClass, Id);
                        break;
                    case Constants.FACTION_YAKUZA:
                        playerClass.SpawnPlayer(Constants.FACTION_YAKUZA_SPAWN, Delay);
                        LoadGangSkin(playerClass, Id);
                        break;
                    case Constants.FACTION_FIB:
                        playerClass.SpawnPlayer(Constants.FACTION_FIB_SPAWN, Delay);
                        LoadGangSkin(playerClass, Id);
                        break;
                    case Constants.FACTION_NARCOS:
                        playerClass.SpawnPlayer(Constants.FACTION_NARCOS_SPAWN, Delay);
                        LoadGangSkin(playerClass, Id);
                        break;
                    case Constants.FACTION_USARMY:
                        playerClass.SpawnPlayer(Constants.FACTION_USARMY_SPAWN, Delay);
                        LoadGangSkin(playerClass, Id);
                        break;
                    case Constants.FACTION_SAMCRO:
                        playerClass.SpawnPlayer(Constants.FACTION_SAMCRO_SPAWN, Delay);
                        LoadGangSkin(playerClass, Id);
                        break;
                    case Constants.FACTION_ROLLINHEIGHTS:
                        playerClass.SpawnPlayer(Constants.FACTION_ROLLINHEIGHTS_SPAWN, Delay);
                        LoadGangSkin(playerClass, Id);
                        break;
                    case Constants.FACTION_COMPTONFAMILYS:
                        playerClass.SpawnPlayer(Constants.FACTION_COMPTONFAMILYS_SPAWN, Delay);
                        LoadGangSkin(playerClass, Id);
                        break;
                }
                playerClass.Health = 200;
                playerClass.Armor = 200;
            }
            catch { }
        }


        public static void GiveRandomHair(PlayerModel playerClass)
        {
            try
            {
                Random random = new Random();
                int randomid = random.Next(0, 18);
                switch (randomid)
                {
                    case 0:
                        playerClass.SetClothes(2, 6, 4, 0);
                        break;
                    case 1:
                        playerClass.SetClothes(2, 7, 4, 0);
                        break;
                    case 2:
                        playerClass.SetClothes(2, 2, 3, 0);
                        break;
                    case 3:
                        playerClass.SetClothes(2, 2, 4, 0);
                        break;
                    case 4:
                        playerClass.SetClothes(2, 14, 4, 0);
                        break;
                    case 5:
                        playerClass.SetClothes(2, 25, 3, 0);
                        break;
                    case 7:
                        playerClass.SetClothes(2, 38, 4, 0);
                        break;
                    case 8:
                        playerClass.SetClothes(2, 57, 3, 0);
                        break;
                    case 9:
                        playerClass.SetClothes(2, 57, 4, 0);
                        break;
                    case 10:
                        playerClass.SetClothes(2, 72, 3, 0);
                        break;
                    case 11:
                        playerClass.SetClothes(2, 72, 4, 0);
                        break;
                    case 12:
                        playerClass.SetClothes(2, 74, 3, 0);
                        break;
                    case 13:
                        playerClass.SetClothes(2, 54, 4, 0);
                        break;
                    case 14:
                        playerClass.SetClothes(2, 32, 4, 0);
                        break;
                    case 15:
                        playerClass.SetClothes(2, 32, 3, 0);
                        break;
                    case 16:
                        playerClass.SetClothes(2, 8, 4, 0);
                        break;
                    case 17:
                        playerClass.SetClothes(2, 8, 2, 0);
                        break;
                    case 18:
                        playerClass.SetClothes(2, 8, 2, 0);
                        break;
                }
            }
            catch (Exception ex) { Debug.CatchExceptions("RandomHair", ex); }
        }


        public static void LoadGangSkin(PlayerModel playerClass, int Gang)
        {
            try
            {
                playerClass.SetPlayerSkin(Alt.Hash("mp_m_freemode_01"));
                playerClass.SetClothes(0, 0, 0, 0);
                playerClass.SetClothes(1, 0, 0, 0);
                playerClass.SetClothes(2, 0, 0, 0);
                playerClass.SetClothes(3, 0, 0, 0);
                playerClass.SetClothes(4, 0, 0, 0);
                playerClass.SetClothes(5, 0, 0, 0);
                playerClass.SetClothes(6, 0, 0, 0);
                playerClass.SetClothes(7, 0, 0, 0);
                playerClass.SetClothes(8, 0, 0, 0);
                playerClass.SetClothes(9, 0, 0, 0);
                playerClass.SetClothes(10, 0, 0, 0);
                playerClass.SetClothes(11, 0, 0, 0);
                GiveRandomHair(playerClass);
                switch (Gang)
                {
                    case Constants.FACTION_LSPD:
                        playerClass.SetClothes(0, 0, 0, 0);
                        playerClass.SetClothes(1, 0, 0, 0);
                        playerClass.SetClothes(2, -1, -1, 0);
                        playerClass.SetClothes(3, 0, 0, 0);
                        playerClass.SetClothes(4, 35, 0, 0);
                        playerClass.SetClothes(5, 0, 0, 0);
                        playerClass.SetClothes(6, 25, 0, 0);
                        playerClass.SetClothes(7, 0, 0, 0);
                        playerClass.SetClothes(8, 58, 0, 0);
                        playerClass.SetClothes(9, 0, 0, 0);
                        playerClass.SetClothes(10, 0, 0, 0);
                        playerClass.SetClothes(11, 55, 0, 0);
                        break;
                    case Constants.FACTION_LCN:
                        playerClass.SetClothes(0, 0, 0, 0);
                        playerClass.SetClothes(1, 0, 0, 0);
                        playerClass.SetClothes(2, -1, -1, 0);
                        playerClass.SetClothes(3, 4, 0, 0);
                        playerClass.SetClothes(4, 28, 0, 0);
                        playerClass.SetClothes(5, 0, 0, 0);
                        playerClass.SetClothes(6, 21, 0, 0);
                        playerClass.SetClothes(7, 0, 0, 0);
                        playerClass.SetClothes(8, 33, 0, 0);
                        playerClass.SetClothes(9, 0, 0, 0);
                        playerClass.SetClothes(10, 0, 0, 0);
                        playerClass.SetClothes(11, 29, 0, 0);
                        break;
                    case Constants.FACTION_YAKUZA:
                        playerClass.SetClothes(2, -1, -1, 0);
                        playerClass.SetClothes(5, 0, 0, 0);
                        playerClass.SetClothes(7, 0, 0, 0);
                        playerClass.SetClothes(8, 15, 0, 0);
                        playerClass.SetClothes(9, 0, 0, 0);
                        playerClass.SetClothes(10, 0, 0, 0);
                        playerClass.SetClothes(0, 0, 0, 0);
                        playerClass.SetClothes(1, -1, 0, 0);
                        playerClass.SetClothes(11, 107, 2, 0);
                        playerClass.SetClothes(3, 33, 0, 0);
                        playerClass.SetClothes(4, 33, 0, 0);
                        playerClass.SetClothes(6, 81, 0, 0);
                        break;
                    case Constants.FACTION_FIB:
                        playerClass.SetClothes(0, -1, -1, 0);
                        playerClass.SetClothes(1, 0, 0, 0);
                        playerClass.SetClothes(2, -1, -1, 0);
                        playerClass.SetClothes(3, 11, 0, 0);
                        playerClass.SetClothes(4, 10, 0, 0);
                        playerClass.SetClothes(5, -1, -1, 0);
                        playerClass.SetClothes(6, 10, 0, 0);
                        playerClass.SetClothes(7, 12, 2, 0);
                        playerClass.SetClothes(8, 15, 0, 0);
                        playerClass.SetClothes(9, 0, 0, 0);
                        playerClass.SetClothes(10, -1, 0, 0);
                        playerClass.SetClothes(11, 13, 0, 0);
                        break;
                    case Constants.FACTION_NARCOS:
                        playerClass.SetClothes(0, 8, -1, 0);
                        playerClass.SetClothes(1, 0, 0, 0);
                        playerClass.SetClothes(2, -1, -1, 0);
                        playerClass.SetClothes(3, 11, 0, 0);
                        playerClass.SetClothes(4, 22, 0, 0);
                        playerClass.SetClothes(5, 0, -1, 0);
                        playerClass.SetClothes(6, 21, 5, 0);
                        playerClass.SetClothes(7, 0, 0, 0);
                        playerClass.SetClothes(8, 15, 0, 0);
                        playerClass.SetClothes(9, 0, 0, 0);
                        playerClass.SetClothes(10, 0, 0, 0);
                        playerClass.SetClothes(11, 13, 1, 0);
                        break;
                    case Constants.FACTION_USARMY:
                        playerClass.SetClothes(4, 86, 8, 0);
                        playerClass.SetClothes(11, 220, 8, 0);
                        playerClass.SetClothes(8, 130, 0, 0);
                        playerClass.SetClothes(6, 24, 0, 0);
                        playerClass.SetClothes(2, 2, 4, 0);
                        playerClass.SetProp(0, 107, 8);
                        break;
                    case Constants.FACTION_SAMCRO:
                        playerClass.SetClothes(0, -1, -1, 0);
                        playerClass.SetClothes(1, 0, 0, 0);
                        playerClass.SetClothes(2, -1, -1, 0);
                        playerClass.SetClothes(3, 2, 0, 0);
                        playerClass.SetClothes(4, 76, 1, 0);
                        playerClass.SetClothes(5, -1, -1, 0);
                        playerClass.SetClothes(6, 25, 0, 0);
                        playerClass.SetClothes(7, 0, 0, 0);
                        playerClass.SetClothes(8, 14, 0, 0);
                        playerClass.SetClothes(9, 0, 0, 0);
                        playerClass.SetClothes(10, -1, 0, 0);
                        playerClass.SetClothes(11, 175, 3, 0);
                        break;
                    case Constants.FACTION_ROLLINHEIGHTS:
                        playerClass.SetClothes(11, 306, 9, 0);
                        playerClass.SetClothes(8, 15, 0, 0);
                        playerClass.SetClothes(6, 26, 0, 0);
                        playerClass.SetClothes(4, 31, 0, 0);
                        playerClass.SetClothes(1, 111, 2, 0);
                        playerClass.SetClothes(3, 4, 0, 0);
                        break;
                    case Constants.FACTION_COMPTONFAMILYS:
                        playerClass.SetClothes(1, 111, 0, 0);
                        playerClass.SetClothes(3, 4, 0, 0);
                        playerClass.SetClothes(4, 31, 0, 0);
                        playerClass.SetClothes(6, 7, 0, 0);
                        playerClass.SetClothes(11, 143, 0, 0);
                        playerClass.SetClothes(8, 15, 0, 0);
                        break;
                }
            }
            catch { }
        }

        public static void GivePlayerGangwarWeapons(PlayerModel player)
        {
            try
            {
                Alt.Log("Try adding weapons to player: " + player.Name);

                player.GiveWeapon(AltV.Net.Enums.WeaponModel.DoubleActionRevolver, 9999, false);
                

                player.GiveWeapon(AltV.Net.Enums.WeaponModel.HeavyRevolverMkII, 9999, false);
                player.AddWeaponComponent(AltV.Net.Enums.WeaponModel.HeavyRevolverMkII, 77277509); // COMPONENT_AT_SCOPE_MACRO_MK2
                player.AddWeaponComponent(AltV.Net.Enums.WeaponModel.HeavyRevolverMkII, 899381934); // COMPONENT_AT_PI_FLSH
                player.SetWeaponTintIndex(AltV.Net.Enums.WeaponModel.HeavyRevolverMkII, 7); // Platinum Teint

                player.GiveWeapon(AltV.Net.Enums.WeaponModel.Pistol50, 9999, false);
                player.GiveWeapon(AltV.Net.Enums.WeaponModel.MiniSMG, 9999, false);
                player.GiveWeapon(AltV.Net.Enums.WeaponModel.SMG, 9999, false);
                player.GiveWeapon(AltV.Net.Enums.WeaponModel.PumpShotgun, 9999, false);
                player.GiveWeapon(AltV.Net.Enums.WeaponModel.Musket, 9999, false);
                player.GiveWeapon(AltV.Net.Enums.WeaponModel.APPistol, 9999, false);
                player.GiveWeapon(AltV.Net.Enums.WeaponModel.CombatPDW, 9999, false);
                player.GiveWeapon(AltV.Net.Enums.WeaponModel.BullpupRifle, 9999, false);
                player.GiveWeapon(AltV.Net.Enums.WeaponModel.AdvancedRifle, 9999, false);
                player.GiveWeapon(AltV.Net.Enums.WeaponModel.GusenbergSweeper, 9999, false);
                player.GiveWeapon(AltV.Net.Enums.WeaponModel.AssaultRifle, 9999, false);
                player.GiveWeapon(AltV.Net.Enums.WeaponModel.SpecialCarbine, 9999, false);
                player.GiveWeapon(AltV.Net.Enums.WeaponModel.Switchblade, 9999, false);
                player.GiveWeapon(AltV.Net.Enums.WeaponModel.Nightstick, 9999, false);
                player.GiveWeapon(AltV.Net.Enums.WeaponModel.Machete, 9999, false);
                player.GiveWeapon(AltV.Net.Enums.WeaponModel.BaseballBat, 9999, false);
                
            }
            catch(Exception ex) { Debug.CatchExceptions("Weapon Problem: ", ex);  }
        }
        public static void LoadGangVehicleSpawnpoints(PlayerModel playerClass)
        {
            try
            {
                foreach (VehicleModel vehClasses in Constants.VehicleSpawn)
                {
                    playerClass.Emit("TextLabel:Create", "Press ~b~E ~w~to select a Vehicle.", vehClasses.SpawnPosition.X, vehClasses.SpawnPosition.Y, vehClasses.SpawnPosition.Z, 0, 255, 255, 255, 40);
                }
            }
            catch (Exception ex) { Debug.CatchExceptions("Gang vehicle Spawn Point Problem: ", ex); }
        }
        public static void LoadGangAreas(PlayerModel playerClass)
        {
            try
            {
                playerClass.Emit("Zone:Create", Constants.FACTION_LSPD_NAME, Constants.FACTION_LSPD_SPAWN.X, Constants.FACTION_LSPD_SPAWN.Y, Constants.FACTION_LSPD_SPAWN.Z, 100, 3, 0);
                playerClass.Emit("Zone:Create", Constants.FACTION_LCN_NAME, Constants.FACTION_LCN_SPAWN.X, Constants.FACTION_LCN_SPAWN.Y, Constants.FACTION_LCN_SPAWN.Z, 100, 55, 0);
                playerClass.Emit("Zone:Create", Constants.FACTION_YAKUZA_NAME, Constants.FACTION_YAKUZA_SPAWN.X, Constants.FACTION_YAKUZA_SPAWN.Y, Constants.FACTION_YAKUZA_SPAWN.Z, 100, 1, 0);
                playerClass.Emit("Zone:Create", Constants.FACTION_FIB_NAME, Constants.FACTION_FIB_SPAWN.X, Constants.FACTION_FIB_SPAWN.Y, Constants.FACTION_FIB_SPAWN.Z, 100, 63, 0);
                playerClass.Emit("Zone:Create", Constants.FACTION_NARCOS_NAME, Constants.FACTION_NARCOS_SPAWN.X, Constants.FACTION_NARCOS_SPAWN.Y, Constants.FACTION_NARCOS_SPAWN.Z, 100, 45, 0);
                playerClass.Emit("Zone:Create", Constants.FACTION_USARMY_NAME, Constants.FACTION_USARMY_SPAWN.X, Constants.FACTION_USARMY_SPAWN.Y, Constants.FACTION_USARMY_SPAWN.Z, 100, 69, 0);
                playerClass.Emit("Zone:Create", Constants.FACTION_SAMCRO_NAME, Constants.FACTION_SAMCRO_SPAWN.X, Constants.FACTION_SAMCRO_SPAWN.Y, Constants.FACTION_SAMCRO_SPAWN.Z, 100, 56, 0);
                playerClass.Emit("Zone:Create", Constants.FACTION_ROLLINHEIGHTS_NAME, Constants.FACTION_ROLLINHEIGHTS_SPAWN.X, Constants.FACTION_ROLLINHEIGHTS_SPAWN.Y, Constants.FACTION_ROLLINHEIGHTS_SPAWN.Z, 100, 27, 0);
                playerClass.Emit("Zone:Create", Constants.FACTION_COMPTONFAMILYS_NAME, Constants.FACTION_COMPTONFAMILYS_SPAWN.X, Constants.FACTION_COMPTONFAMILYS_SPAWN.Y, Constants.FACTION_COMPTONFAMILYS_SPAWN.Z, 80, 2, 340);
            }
            catch (Exception ex) { Debug.CatchExceptions("Zone Problem: ", ex); }
        }
    }
}
