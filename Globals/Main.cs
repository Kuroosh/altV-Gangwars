using Gangwars.Model;
using System.Collections.Generic;

namespace Gangwars.Globals
{
    public class Main
    {
        public static List<RegisterModel> RegisteredAccounts;
        public static void OnResourceStart()
        {
            Database.Main.OnResourceStart();
        }
    }
}
