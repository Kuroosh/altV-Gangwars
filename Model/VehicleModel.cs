using System.Numerics;

namespace Gangwars.Model
{
    public class VehicleModel
    {
        public string FactionName { get; set; }
        public int FactionID { get; set; }
        public int[] VehicleColor { get; set; }
        public Vector3 SpawnPosition { get; set; }
        public Vector3 SpawnRotation { get; set; }
    }
}
