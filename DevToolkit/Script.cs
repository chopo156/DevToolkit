#if SINGLEPLAYER
using GTA;
using System;
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
    }
}
