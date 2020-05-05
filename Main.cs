using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Elements.Entities;
using Gangwars.Model;

namespace Gangwars
{
    internal class GangwarsResource : AsyncResource
    {
        public override IEntityFactory<IPlayer> GetPlayerFactory()
        {
            return new MyPlayerFactory();
        }
        public override void OnStart()
        {
            Globals.Main.OnResourceStart();
        }
        public override void OnStop()
        {
            //
        }
    }
    public class Gangwars
    {
    }
}
