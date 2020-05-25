#if FIVEM
using CitizenFX.Core;
#elif SINGLEPLAYER
using GTA;
using GTA.Math;
using GTA.UI;
using System;
using System.IO;
#endif

namespace DevToolkit
{
    public enum CoordType
    {
        CSharp,
        Lua
    }

    /// <summary>
    /// Tools for quick access to GTA V data in both FiveM and SHVDN.
    /// </summary>
    public static class Tools
    {
        /// <summary>
        /// Gets the coordinates of the local player ped or vehicle.
        /// </summary>
        /// <returns>A Vector3 with the coordinates of the player or player vehicle.</returns>
        public static Vector3 PlayerCoords
        {
            get
            {
                if (Game.Player.Character.CurrentVehicle != null)
                {
                    return Game.Player.Character.CurrentVehicle.Position;
                }
                else
                {
                    return Game.Player.Character.Position;
                }
            }
            set
            {
                if (Game.Player.Character.CurrentVehicle != null)
                {
                    Game.Player.Character.CurrentVehicle.Position = value;
                }
                else
                {
                    Game.Player.Character.Position = value;
                }
            }
        }
        /// <summary>
        /// The heading of the player ped or player vehicle.
        /// </summary>
        public static float Heading
        {
            get
            {
                if (Game.Player.Character.CurrentVehicle != null)
                {
                    return Game.Player.Character.CurrentVehicle.Heading;
                }
                else
                {
                    return Game.Player.Character.Heading;
                }
            }
            set
            {
                if (Game.Player.Character.CurrentVehicle != null)
                {
                    Game.Player.Character.CurrentVehicle.Heading = value;
                }
                else
                {
                    Game.Player.Character.Heading = value;
                }
            }
        }

        /// <summary>
        /// Shows a message for the respective platform.
        /// On FiveM, is printed on the console. For SHVDN, a notification is shown.
        /// </summary>
        /// <param name="message">The message to show.</param>
        public static void ShowMessage(string message)
        {
#if SINGLEPLAYER
            Notification.Show(message);
#elif FIVEM
            Debug.WriteLine(message);
#endif
        }

        /// <summary>
        /// Saves the player coordinates.
        /// </summary>
        public static void SaveCoords(CoordType mode)
        {
            // Select the correct prefix and sufix
            string prefix = "";
            string sufix = "";
            switch (mode)
            {
                case CoordType.CSharp:
                    prefix = "[C#] new Vector3";
                    sufix = ";";
                    break;
                case CoordType.Lua:
                    prefix = "[Lua] vector3";
                    break;
            }

            // Get the coordenates and show them in the correct format 
            Vector3 coords = PlayerCoords;
            string format = $"{prefix}({coords.X}, {coords.Y}, {coords.Z}){sufix}";
            ShowMessage(format);
#if SINGLEPLAYER
            // On SP, manually save it into a text file
            File.AppendAllText("scripts\\DevToolkit.Coords.txt", format + Environment.NewLine);
#endif
        }
    }
}
