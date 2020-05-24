#if FIVEM
using CitizenFX.Core;
#elif SINGLEPLAYER
using GTA;
using GTA.Math;
using GTA.UI;
#endif

namespace DevToolkit
{
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
    }
}
