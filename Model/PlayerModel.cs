using AltV.Net;
using AltV.Net.Elements.Entities;
using System;
using System.Numerics;

namespace Gangwars.Model
{
    public class PlayerModel : Player
    {
        public int _UID { get; set; }
        public string _Name { get; set; }
        public int _Playtime { get; set; }
        public bool _LoggedIn { get; set; }
        public int _Kills { get; set; }
        public int _Deaths { get; set; }
        public int _MaxStreaks { get; set; }
        public int _Cstreak { get; set; }
        public int _Level { get; set; }
        public int _EXP { get; set; }
        public int _Alevel { get; set; }
        public Vector3 position
        {
            get { return Position; }
            set { Alt.Emit("GlobalSystems:PlayerPosition", this, value); }
        }

        public PlayerModel(IServer server, IntPtr nativePointer, ushort id) : base(server, nativePointer, id)
        {
            try
            {
                _Name = "NULL-NO-NAME";
                _Playtime = 0;
                _Kills = 0;
                _Deaths = 0;
                _MaxStreaks = 0;
                _Cstreak = 0;
                _Alevel = 0;
                _Level = 1;
                _EXP = 0;
                _LoggedIn = false;
                position = new Vector3(0, 0, 0);
            }
            catch (Exception ex) { Core.Debug.CatchExceptions("PlayerModel-Create", ex); }
        }
    }
    public class MyPlayerFactory : IEntityFactory<IPlayer>
    {
        public IPlayer Create(IServer server, IntPtr entityPointer, ushort id)
        {
            try
            {
                return new PlayerModel(server, entityPointer, id);
            }
            catch (Exception ex) { Core.Debug.CatchExceptions("PlayerFactory:Create", ex); return null; }
        }
    }
}
