#if FIVEM
using CitizenFX.Core;
using System;
#elif SINGLEPLAYER
using GTA;
using GTA.Math;
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
    }
}
