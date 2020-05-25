#if SINGLEPLAYER
using GTA;
using GTA.Math;
using System;
using System.Diagnostics;
using System.Reflection;
#elif FIVEM
using CitizenFX.Core;
using CitizenFX.Core.Native;
using Script = CitizenFX.Core.BaseScript;
#endif
using System.Collections.Generic;

namespace DevToolkit
{
    /// <summary>
    /// Script used by DevToolkit to provide tools.
    /// </summary>
    public class Commands : Script
    {
        public Commands()
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
                                    if (string.IsNullOrWhiteSpace(param))
                                    {
                                        Tools.ShowMessage("The command was cancelled");
                                        return;
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
            Tools.SaveCoords(CoordType.CSharp);
        }

        /// <summary>
        /// Gets the coordenates and prints them in the Lua format (vector3).
        /// </summary>
        [Command("luacoords")]
        public void CoordsLuaCommand(int source, List<object> parameters, string raw)
        {
            Tools.SaveCoords(CoordType.Lua);
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

        /// <summary>
        /// Spawns a vehicle for the player.
        /// </summary>
        [Command("spawn")]
        [Parameters(1)]
        public void SpawnCommand(int source, List<object> parameters, string raw)
        {
            // If no model name was specified, return
            if (parameters.Count == 0)
            {
                Tools.ShowMessage("You need to specify a model name!");
                return;
            }

            // Then, spawn the vehicle directly on singleplayer or ask the server on FiveM
#if SINGLEPLAYER
            Tools.SpawnVehicle(parameters[0].ToString());
#elif FIVEM
            TriggerServerEvent("devtoolkit:spawnVehicle", parameters[0].ToString());
#endif
        }

        /// <summary>
        /// Changes the model of the player ped.
        /// </summary>
        [Command("model")]
        [Parameters(1)]
        public void ModelCommand(int source, List<object> parameters, string raw)
        {
            // If no model name was specified, return
            if (parameters.Count == 0)
            {
                Tools.ShowMessage("You need to specify a model name!");
                return;
            }

            // Then, change the player model directly on singleplayer or ask the server on FiveM
#if SINGLEPLAYER
            Tools.ChangePlayerModel(parameters[0].ToString());
#elif FIVEM
            TriggerServerEvent("devtoolkit:changeModel", parameters[0].ToString());
#endif
        }

        /// <summary>
        /// Gives the player all of the known weapons.
        /// </summary
        [Command("giveall")]
        public void GiveWeapons(int source, List<object> parameters, string raw)
        {
#if SINGLEPLAYER
            Tools.GiveAllWeapons();
#elif FIVEM
            TriggerServerEvent("devtoolkit:giveWeapons");
#endif
        }

        /// <summary>
        /// Gives the player all of the known weapons.
        /// </summary
        [Command("give")]
        [Parameters(1)]
        public void GiveWeapon(int source, List<object> parameters, string raw)
        {
            // If no weapon name was specified, return
            if (parameters.Count == 0)
            {
                Tools.ShowMessage("You need to specify the name of the weapon!");
                return;
            }

#if SINGLEPLAYER
            Tools.GiveWeapon(parameters[0].ToString());
#elif FIVEM
            TriggerServerEvent("devtoolkit:giveWeapon", parameters[0].ToString());
#endif
        }

        /// <summary>
        /// Deletes the current player vehicle.
        /// </summary>
        [Command("dv")]
        public void DeleteVehicleCommand(int source, List<object> parameters, string raw)
        {
#if SINGLEPLAYER
            Game.Player.Character.CurrentVehicle?.Delete();
#elif FIVEM
            TriggerServerEvent("devtoolkit:deleteVehicle");
#endif
        }

        /// <summary>
        /// Repairs the current player vehicle.
        /// </summary>
        [Command("fix")]
        public void FixVehicleCommand(int source, List<object> parameters, string raw)
        {
#if SINGLEPLAYER
            Game.Player.Character.CurrentVehicle?.Repair();
#elif FIVEM
            TriggerServerEvent("devtoolkit:fixVehicle");
#endif
        }

        /// <summary>
        /// Plays a specific sound from a specific bank.
        /// </summary>
        [Command("playsound")]
        [Parameters(2)]
        public void PlaySoundCommand(int source, List<object> parameters, string raw)
        {
            // If the user forgot to enter the bank or sound, return
            if (parameters.Count < 2)
            {
                Tools.ShowMessage("You need to specify the audio name and bank!");
                return;
            }

            // Otherwise trigger the sound
#if SINGLEPLAYER
            Tools.PlaySound(parameters[0].ToString(), parameters[1].ToString());
#elif FIVEM
            TriggerServerEvent("devtoolkit:playSound", parameters[0].ToString(), parameters[1].ToString());
#endif
        }

#if SINGLEPLAYER
        /// <summary>
        /// Terminates the game.
        /// </summary>
        [Command("quit")]
        public void QuitCommand(int source, List<object> parameters, string raw)
        {
            Process.Start("taskkill", "/IM GTA5.exe /F");
        }
#endif
    }
}
