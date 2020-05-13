using AltV.Net.Data;
using Gangwars.Model;
using System.Collections.Generic;
using System.Numerics;

namespace Gangwars.Globals
{
    public class Constants
    {
        public const string FACTION_LSPD_NAME = "L.S.P.D";
        public const string FACTION_LCN_NAME = "La Cosa Nostra";
        public const string FACTION_YAKUZA_NAME = "Yakuza";
        public const string FACTION_FIB_NAME = "F.I.B";
        public const string FACTION_NARCOS_NAME = "Narcos";
        public const string FACTION_USARMY_NAME = "U.S - Army";
        public const string FACTION_SAMCRO_NAME = "Samcro Redwoods";
        public const string FACTION_ROLLINHEIGHTS_NAME = "Rollin Height's Ballas";
        public const string FACTION_COMPTONFAMILYS_NAME = "Compton Family's";

        public const int ADMINLVL_NONE = 0;
        public const int ADMINLVL_SUPPORTER = 1;
        public const int ADMINLVL_MODERATOR = 2;
        public const int ADMINLVL_ADMINISTRATOR = 3;

        public const string BANTYPE_TIMEBAN = "Timeban";
        public const string BANTYPE_PERMABAN = "Permanent";

        public const int FACTION_LSPD = 1;
        public const int FACTION_LCN = 2;
        public const int FACTION_YAKUZA = 3;
        public const int FACTION_FIB = 6;
        public const int FACTION_NARCOS = 7;
        public const int FACTION_USARMY = 8;
        public const int FACTION_SAMCRO = 9;
        public const int FACTION_ROLLINHEIGHTS = 12;
        public const int FACTION_COMPTONFAMILYS = 13;

        public static Vector3 FACTION_LSPD_SPAWN = new Vector3(428.24176f, -980.8088f, 30.69519f);
        public static Vector3 FACTION_LCN_SPAWN = new Vector3(-1038.044f, 222.168f, 64.37566f);
        public static Vector3 FACTION_YAKUZA_SPAWN = new Vector3(-1516.701f, 851.7410f, 181.5947f);
        public static Vector3 FACTION_FIB_SPAWN = new Vector3(77.02418f, -739.2659f, 45.08496f);
        public static Vector3 FACTION_NARCOS_SPAWN = new Vector3(-1549.369f, -91.01895f, 54.92917f);
        public static Vector3 FACTION_USARMY_SPAWN = new Vector3(-1108.0088f, -845.0901f, 19.30481f);
        public static Vector3 FACTION_SAMCRO_SPAWN = new Vector3(982.0083f, -100.8747f, 74.84512f);
        public static Vector3 FACTION_ROLLINHEIGHTS_SPAWN = new Vector3(-204.68571f, -1604.1758f, 34.823486f);
        public static Vector3 FACTION_COMPTONFAMILYS_SPAWN = new Vector3(126.6941f, -1930.021f, 21.38243f);

        public static Dictionary<int, AltV.Net.Enums.VehicleModel> VehicleLevelList = new Dictionary<int, AltV.Net.Enums.VehicleModel>
        {
            {   1, AltV.Net.Enums.VehicleModel.Voodoo },
            {   2, AltV.Net.Enums.VehicleModel.Felon },
            {   3, AltV.Net.Enums.VehicleModel.Jackal },
            {   4, AltV.Net.Enums.VehicleModel.F620 },
            {   5, AltV.Net.Enums.VehicleModel.Oracle2 },
            {   6, AltV.Net.Enums.VehicleModel.Sultan2 },
            {   7, AltV.Net.Enums.VehicleModel.Sultan },
            {   8, AltV.Net.Enums.VehicleModel.Tulip },
            {   9, AltV.Net.Enums.VehicleModel.Drafter },
            {   10, AltV.Net.Enums.VehicleModel.Comet2 },
            {   11, AltV.Net.Enums.VehicleModel.Kuruma },
            {   12, AltV.Net.Enums.VehicleModel.Buffalo },
            {   13, AltV.Net.Enums.VehicleModel.Cheetah2 },
            {   14, AltV.Net.Enums.VehicleModel.Kamacho },
            {   15, AltV.Net.Enums.VehicleModel.Brawler },
            {   16, AltV.Net.Enums.VehicleModel.Bati },
            {   17, AltV.Net.Enums.VehicleModel.Dominator3 },
            {   18, AltV.Net.Enums.VehicleModel.Jester },
            {   19, AltV.Net.Enums.VehicleModel.Elegy },
            {   20, AltV.Net.Enums.VehicleModel.Marshall },
            {   21, AltV.Net.Enums.VehicleModel.Hakuchou },
            {   22, AltV.Net.Enums.VehicleModel.Caracara2 },
            {   23, AltV.Net.Enums.VehicleModel.Voltic },
            {   24, AltV.Net.Enums.VehicleModel.Infernus },
            {   25, AltV.Net.Enums.VehicleModel.Ninef },
            {   26, AltV.Net.Enums.VehicleModel.Komoda },
            {   27, AltV.Net.Enums.VehicleModel.Imorgon },
            {   28, AltV.Net.Enums.VehicleModel.Schafter2 },
            {   29, AltV.Net.Enums.VehicleModel.Schafter3 },
            {   30, AltV.Net.Enums.VehicleModel.Emerus },
            {   31, AltV.Net.Enums.VehicleModel.Cyclone },
            {   32, AltV.Net.Enums.VehicleModel.Reaper },
            {   33, AltV.Net.Enums.VehicleModel.Tempesta },
            {   34, AltV.Net.Enums.VehicleModel.Taipan },
            {   35, AltV.Net.Enums.VehicleModel.Vagner },
            {   36, AltV.Net.Enums.VehicleModel.Visione },
            {   37, AltV.Net.Enums.VehicleModel.T20 },
            {   38, AltV.Net.Enums.VehicleModel.Xa21 },
            {   39, AltV.Net.Enums.VehicleModel.Prototipo },
            {   40, AltV.Net.Enums.VehicleModel.Thrax },
        };


        public static List<VehicleModel> VehicleSpawn = new List<VehicleModel>
        {
            new VehicleModel
            {
                FactionID = FACTION_LSPD,
                FactionName = FACTION_LSPD_NAME,
                SpawnPosition = new Vector3(408.8835f, -980.0571f, 29.263062f),
                SpawnRotation = new Rotation(0,0,0),
                VehicleColor = new int[]{ 255,255,255}
            },
            new VehicleModel
            {
                FactionID = FACTION_LCN,
                FactionName = FACTION_LCN_NAME,
                SpawnPosition = new Vector3(-1045.4901f, 207.87692f,63.046875f),
                SpawnRotation = new Rotation(0,0,0),
                VehicleColor = new int[]{ 0,0,0}
            },
            new VehicleModel
            {
                FactionID = FACTION_YAKUZA,
                FactionName = FACTION_YAKUZA_NAME,
                SpawnPosition = new Vector3(-1518.6461f,865.1868f,181.6864f),
                SpawnRotation = new Rotation(0,0,0),
                VehicleColor = new int[]{ 255, 0, 0 }
            },
            new VehicleModel
            {
                FactionID = FACTION_FIB,
                FactionName = FACTION_FIB_NAME,
                SpawnPosition = new Vector3(48.356045f,-730.1934f,44.309937f),
                SpawnRotation = new Rotation(0,0,0),
                VehicleColor = new int[]{ 0, 0, 0}
            },
            new VehicleModel
            {
                FactionID = FACTION_NARCOS,
                FactionName = FACTION_NARCOS_NAME,
                SpawnPosition = new Vector3(-1542.5406f,-79.22637f,54.1333f),
                SpawnRotation = new Rotation(0,0,0),
                VehicleColor = new int[]{ 150, 150, 150 }
            },
            new VehicleModel
            {
                FactionID = FACTION_USARMY,
                FactionName = FACTION_USARMY_NAME,
                SpawnPosition = new Vector3(-1128.7517f, -816.9099f, 15.901123f),
                SpawnRotation = new Rotation(0,0,0),
                VehicleColor = new int[]{ 0, 0, 0 }
            },
            new VehicleModel
            {
                FactionID = FACTION_SAMCRO,
                FactionName = FACTION_SAMCRO_NAME,
                SpawnPosition = new Vector3(979.4242f,-114.56703f,74.23511f),
                SpawnRotation = new Rotation(0,0,0),
                VehicleColor = new int[]{ 0, 0, 0 }
            },
            new VehicleModel
            {
                FactionID = FACTION_ROLLINHEIGHTS,
                FactionName = FACTION_ROLLINHEIGHTS_NAME,
                SpawnPosition = new Vector3(-188.88791f, -1610.5187f,33.8125f),
                SpawnRotation = new Rotation(0,0,0),
                VehicleColor = new int[]{ 138, 43, 226 }

            },
            new VehicleModel
            {
                FactionID = FACTION_COMPTONFAMILYS,
                FactionName = FACTION_COMPTONFAMILYS_NAME,
                SpawnPosition = new Vector3(114.10549f,-1933.5692f,20.619019f),
                SpawnRotation = new Rotation(0,0,0),
                VehicleColor = new int[]{ 0, 86, 0 }
            },

        };
    }
}
