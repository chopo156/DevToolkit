#if FIVEM
using CitizenFX.Core;
using System;
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
        /// Gets the coordinates of the local player.
        /// </summary>
        /// <returns>A Vector3 with the coordinates of the player.</returns>
        public static Vector3 PlayerCoords
        {
            get
            {
#if (CLIENT || SINGLEPLAYER)
                return Game.Player.Character.Position;
#else
                throw new InvalidOperationException("Player Position can't be fetched.");
#endif
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
