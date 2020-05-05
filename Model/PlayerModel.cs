using AltV.Net;
using AltV.Net.Elements.Entities;
using System;
using System.Numerics;

namespace Gangwars.Model
{
    public class PlayerModel : Player
    {
        public string _Name { get; set; }
        public int _Playtime { get; set; }
        public int _Kills { get; set; }
        public int _Deaths { get; set; }
        public int _MaxStreaks { get; set; }
        public int _Cstreak { get; set; }
        public int _Alevel { get; set; }
        private Vector3 Pos { get; set; }
        public Vector3 position
        {
            get { return Pos; }
            set { Pos = value; Alt.Emit("GlobalSystems:PlayerPosition", this, value); Core.Debug.OutputDebugString("Called Pos " + value); }
        }
        public PlayerModel(IntPtr nativePointer, ushort id) : base(nativePointer, id)
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
                position = new Vector3(0, 0, 500);
            }
            catch (Exception ex) { Core.Debug.CatchExceptions("PlayerModel-Create", ex); }
        }
    }
    public class MyPlayerFactory : IEntityFactory<IPlayer>
    {
        public IPlayer Create(IntPtr playerPointer, ushort id)
        {
            try
            {
                return new PlayerModel(playerPointer, id);
            }
            catch (Exception ex) { Core.Debug.CatchExceptions("PlayerFactory:Create", ex); return null; }
        }
    }
}
