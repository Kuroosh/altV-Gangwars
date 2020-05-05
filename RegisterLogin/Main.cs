using AltV.Net;
using Gangwars.Core;
using Gangwars.Model;
using System.Numerics;

namespace Gangwars.RegisterLogin
{
    public class Main : IScript
    {
        public static void ShowRegisterLogin(PlayerModel playerClass)
        {
            playerClass.Emit("LoginRegister:Create");
        }
        [ClientEvent("Gangwars:Login")]
        public void OnPlayerLoginTry(PlayerModel playerClass, string name, string password)
        {
            string passwordHashed = Core.Debug.Sha256(password);
            foreach (RegisterModel registeredClass in Globals.Main.RegisteredAccounts)
            {
                if (registeredClass.Username.ToLower() == name.ToLower() && registeredClass.Password == passwordHashed)
                {
                    playerClass.Emit("LoginRegister:Destroy");
                    playerClass.Emit("TeamSelection:Show");
                }
            }
        }
        [ClientEvent("Gangwars:Register")]
        public void OnPlayerRegisterTry(PlayerModel playerClass, string name, string password)
        {
            string passwordHashed = Core.Debug.Sha256(password);
            if (Globals.Main.RegisteredAccounts.Count > 0)
            {
                foreach (RegisterModel registeredClass in Globals.Main.RegisteredAccounts)
                {
                    if (registeredClass.Username.ToLower() == name.ToLower())
                    {
                        Core.Debug.OutputDebugString("Der Nutzername exestiert bereits!!");
                        return;
                    }
                    if (registeredClass.HardwareId.ToString() == playerClass.HardwareIdHash.ToString())
                    {
                        Core.Debug.OutputDebugString("Du hast bereits einen Account!");
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
        }

        [ClientEvent("Gangwars:SelectTeam")]
        public void SelectTeam(PlayerModel playerClass, int team)
        {
            playerClass.SetPlayerSkin(Alt.Hash("mp_m_freemode_01"));
            switch (team)
            {
                case 1:
                    playerClass.SpawnPlayer(new Vector3(0, 0, 72));
                    break;
                case 2:
                    playerClass.SpawnPlayer(new Vector3(0, 0, 72));
                    break;
                case 3:
                    playerClass.SpawnPlayer(new Vector3(0, 0, 72));
                    break;
                case 4:
                    playerClass.SpawnPlayer(new Vector3(0, 0, 72));
                    break;
            }
        }
    }
}
