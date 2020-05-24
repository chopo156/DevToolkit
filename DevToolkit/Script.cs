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
using System.Collections.Generic;

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
                    // If the command was entered as a cheat
                    if (Game.WasCheatStringJustEntered(command.Command))
                    {
                        // Create the list of parameters
                        List<object> userParams = new List<object>();

                        // Check if we need to request parameters
                        if (method.GetCustomAttribute(typeof(ParametersAttribute)) is ParametersAttribute parameters)
                        {
                            // If the number of parameters is not zero
                            if (parameters.Count != 0)
                            {
                                // Start asking parameter for parameter
                                for (int i = 0; i < parameters.Count; i++)
                                {
                                    // Ask for the paramter
                                    string param = Game.GetUserInput();
                                    // If is null, operation is cancelled
                                    if (param == null)
                                    {
                                        continue;
                                    }
                                    // Otherwise, add it onto the list
                                    userParams.Add(param);
                                }
                            }
                        }

                        // Finally, execute the command with the correct parameters
                        method.Invoke(this, new object[]
                        {
                            0,
                            userParams,
                            command.Command + " " + string.Join(" ", userParams)
                        });
                    }
                }
            }
        }
#endif

        /// <summary>
        /// Gets the coordenates and prints them in the C# format (Vector3).
        /// </summary>
        [Command("cscoords")]
        public void CoordsCSCommand(int source, List<object> parameters, string raw)
        {
            // Get the coordenates
            Vector3 coords = Tools.PlayerCoords;
            // Format them
            string format = $"new Vector3({coords.X}, {coords.Y}, {coords.Z})";
            // And show them to the user
            Tools.ShowMessage(format);
#if SINGLEPLAYER
            // On SP, manually save it into a text file
            File.AppendAllText("scripts\\DevToolkit.Coords.txt", format + Environment.NewLine);
#endif
        }

        /// <summary>
        /// Sets the position of the player ped or vehicle.
        /// </summary>
        [Command("setpos")]
        [Parameters(3)]
        public void SetPosCommand(int source, List<object> parameters, string raw)
        {
            // If there are less than 3 parameters (only possible in FiveM), notify it and return
            if (parameters.Count < 3)
            {
                Tools.ShowMessage("You need to specify the X, Y and Z position");
                return;
            }

            // Try to parse the positions
            // If we failed, tell the user and return
            if (!float.TryParse(parameters[0].ToString(), out float x) ||
                !float.TryParse(parameters[1].ToString(), out float y) ||
                !float.TryParse(parameters[2].ToString(), out float z))
            {
                Tools.ShowMessage("One of the parameters is not a valid float!");
                return;
            }

            // For SHVDN, go ahead and set it
            // For FiveM, ask the server for ACL confirmation
#if SINGLEPLAYER
            Tools.PlayerCoords = new Vector3(x, y, z);
#elif FIVEM
            TriggerServerEvent("devtoolkit:setPosition", x, y, z);
#endif
        }
    }
}
