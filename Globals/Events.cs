using AltV.Net;
using Gangwars.Model;
using System;

namespace Gangwars.Globals
{
    public class Events : IScript
    {
        [ScriptEvent(ScriptEventType.PlayerConnect)]
        public void OnPlayerConnect(PlayerModel playerClass, string reason)
        {
            playerClass.OnPlayerConnect();
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
