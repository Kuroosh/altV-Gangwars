using AltV.Net;
using AltV.Net.Resources.Chat.Api;
using Gangwars.Globals;
using Gangwars.Model;
using System;
using System.Numerics;

namespace Gangwars.Admin
{
    public class Main : IScript
    {
        /*
        [Command("setclothes")]
        public static void SetClothes(PlayerModel playerClass, int ClothesSlot, int ClothesDrawable, int ClothesTexture)
        {
            playerClass.SetClothes(ClothesSlot, ClothesDrawable, ClothesTexture);
        }
        [Command("setprop")]
        public static void SetProps(PlayerModel playerClass, int ClothesSlot, int ClothesDrawable, int ClothesTexture)
        {
            playerClass.SetProp(ClothesSlot, ClothesDrawable, ClothesTexture);
        }
        [Command("testvehicle")]
        public static void CreateVehicle(PlayerModel playerClass)
        {
            Alt.CreateVehicle(AltV.Net.Enums.VehicleModel.T20, playerClass.Position, playerClass.Rotation);
        }
        [Command("haircolor")]
        public static void ChangeHairColor(PlayerModel playerClass, int color1, int color2)
        {
            playerClass.Emit("Hair:Color", color1, color2);
        }*/

        [Command("timeban")]
        public static void TimeBanPlayer(PlayerModel admin, string target, int Banhours, string reason)
        {
            try
            {
                if (admin._Alevel < Constants.ADMINLVL_MODERATOR) { return; }
                PlayerModel targetClass = Globals.Main.FindPlayerByName(target);
                if (targetClass == null) { return; }
                targetClass.Kick(reason);
                Database.Main.AddPlayerBan(admin, targetClass, DateTime.Now.AddHours(Banhours), reason, Globals.Constants.BANTYPE_TIMEBAN);
            }
            catch (Exception ex) { Core.Debug.CatchExceptions("TimeBanPlayer", ex); }
        }

        [Command("permaban")]
        public static void PermaBanPlayer(PlayerModel admin, string target, string reason)
        {
            try
            {
                if (admin._Alevel < Constants.ADMINLVL_ADMINISTRATOR) { return; }
                PlayerModel targetClass = Globals.Main.FindPlayerByName(target);
                if (targetClass == null) { return; }
                targetClass.Kick(reason);
                Database.Main.AddPlayerBan(admin, targetClass, DateTime.Now, reason, Globals.Constants.BANTYPE_PERMABAN);
            }
            catch (Exception ex) { Core.Debug.CatchExceptions("PermaBanPlayer", ex); }
        }

        [Command("kick")]
        public static void KickPlayer(PlayerModel admin, string target, string reason)
        {
            try
            {
                if (admin._Alevel < Constants.ADMINLVL_SUPPORTER) { return; }
                PlayerModel targetClass = Globals.Main.FindPlayerByName(target);
                if (targetClass == null) { return; }
                targetClass.Kick(reason);
            }
            catch (Exception ex) { Core.Debug.CatchExceptions("KickPlayer", ex); }
        }

        [Command("goto")]
        public static void GotoTarget(PlayerModel admin, string target)
        {
            try
            {
                if (admin._Alevel < Constants.ADMINLVL_SUPPORTER) { return; }
                PlayerModel targetClass = Globals.Main.FindPlayerByName(target);
                if (targetClass == null) { return; }
                admin.position = targetClass.Position;
            }
            catch (Exception ex) { Core.Debug.CatchExceptions("GotoTarget", ex); }
        }

        [Command("gethere")]
        public static void GethereTarget(PlayerModel admin, string target)
        {
            try
            {
                if (admin._Alevel < Constants.ADMINLVL_SUPPORTER) { return; }
                PlayerModel targetClass = Globals.Main.FindPlayerByName(target);
                if (targetClass == null) { return; }
                targetClass.Position = admin.position;
            }
            catch (Exception ex) { Core.Debug.CatchExceptions("GethereTarget", ex); }
        }



        [Command("pos")]
        public static void GetPlayerPosition(PlayerModel playerClass)
        {
            Vector3 PlayerPos = playerClass.Position;
            string PlayerPosition = PlayerPos.X + " | " + PlayerPos.Y + " | " + PlayerPos.Z;
            string NewPosition = PlayerPosition.Replace(",", ".");
            Core.Debug.OutputDebugString("Position : " + NewPosition);
        }
    }
}
