using System;

namespace DevToolkit
{
#if SINGLEPLAYER

    /// <summary>
    /// Dummy attribute for Commands.
    /// </summary>
    public class CommandAttribute : Attribute
    {
        public string Command { get; set; }
        public bool Restricted { get; set; }

        public CommandAttribute(string command)
        {
            Command = command;
        }
    }

    /// <summary>
    /// Dummy attribute for Ticks. 
    /// </summary>
    public class TickAttribute : Attribute
    {
    }

#endif
}
