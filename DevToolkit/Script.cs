#if SINGLEPLAYER
using GTA;
using GTA.Math;
using GTA.UI;
using System;
using System.IO;
using System.Reflection;
#elif FIVEM
using CitizenFX.Core;
using Script = CitizenFX.Core.BaseScript;
#endif

namespace DevToolkit
{
    /// <summary>
    /// Script used by DevToolkit to provide tools.
    /// </summary>
    public class DevToolkit : Script
    {
        public DevToolkit()
        {
#if SINGLEPLAYER
            // For Singleplayer, we need to add the commands and ticks manually
            Tick += DevToolkit_Tick;
#endif
        }

#if SINGLEPLAYER
        private void DevToolkit_Tick(object sender, EventArgs e)
        {
            // Get all of the methods
            MethodInfo[] methods = GetType().GetMethods();

            // And iterate over them
            foreach (MethodInfo method in methods)
            {
                // If there is a command attribute
                if (method.GetCustomAttribute(typeof(CommandAttribute)) is CommandAttribute command)
                {
                    // If the command was entered as a cheat, execute it
                    if (Game.WasCheatStringJustEntered(command.Command))
                    {
                        method.Invoke(this, new object[0]);
                    }
                }
            }
        }
#endif

        /// <summary>
        /// Gets the coordenates and prints them in the C# format (Vector3).
        /// </summary>
        [Command("cscoords")]
        public void CoordsCSCommand()
        {
#if SERVER
            // For FiveM servers, this command can't be used
            Debug.WriteLine("This command can't be used on Servers");
            return;
#endif

            // Get the coordenates
            Vector3 coords = Tools.PlayerCoords;
            // Format them
            string format = $"new Vector3({coords.X}, {coords.Y}, {coords.Z})";
            // And print them in the correct format
#if SINGLEPLAYER
            // For SP, use a notification in the UI
            Notification.Show(format);
            // And write them into the correct file
            File.AppendAllText("scripts\\DevToolkit.Coords.txt", format + Environment.NewLine);
#elif CLIENT
            // For the FiveM client, print them on the console
            // They will be saved to the client log file
            Debug.WriteLine(format);
#endif
        }
    }
}
