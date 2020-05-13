using AltV.Net;
using AltV.Net.Elements.Entities;
using Gangwars.Core;
using Gangwars.Globals;
using Gangwars.Model;

namespace Gangwars.EventKey
{
    public class Main : IScript
    {
        private static VehicleModel GetNearestVehicleSpawner(PlayerModel playerClass)
        {
            foreach (VehicleModel vehClasses in Globals.Constants.VehicleSpawn)
            {
                if (playerClass.Position.Distance(vehClasses.SpawnPosition) <= 10)
                {
                    return vehClasses;
                }
            }
            return null;
        }
        [ClientEvent("OnPressedEventKey")]
        public static void OnPressedEventKey(PlayerModel playerClass)
        {
            try
            {
                if (playerClass.IsInVehicle) { return; }
                if (GetNearestVehicleSpawner(playerClass) != null)
                {
                    playerClass.Emit("CarWindow:Show", playerClass._Level);
                    Core.Debug.OutputDebugString("CarWindow:Show : " + playerClass.Name);
                }
            }
            catch { }
        }

        [ClientEvent("Car:SpawnVehicle")]
        public static void SpawnPrivateVehicle(PlayerModel playerClass, int numb)
        {
            try
            {
                if (playerClass.IsInVehicle) { return; }
                VehicleModel nearestSpawn = GetNearestVehicleSpawner(playerClass);
                if (nearestSpawn != null)
                {
                    foreach (IVehicle veh in Alt.GetAllVehicles())
                    {
                        if (veh.vnxGetElementData<string>(EntityData.VEHICLE_OWNER) == playerClass.GetVnXName())
                        {
                            veh.Remove();
                        }
                    }
                    IVehicle vehicle = Alt.CreateVehicle(Constants.VehicleLevelList[numb], nearestSpawn.SpawnPosition, nearestSpawn.SpawnRotation);
                    vehicle.vnxSetElementData(EntityData.VEHICLE_OWNER, playerClass.GetVnXName());
                    vehicle.PrimaryColorRgb = new AltV.Net.Data.Rgba((byte)nearestSpawn.VehicleColor[0], (byte)nearestSpawn.VehicleColor[1], (byte)nearestSpawn.VehicleColor[2], 255);
                    RageAPI.WarpIntoVehicle(playerClass, vehicle, -1);
                }
                playerClass.Emit("CarWindow:Hide");
            }
            catch { }
        }
    }
}
