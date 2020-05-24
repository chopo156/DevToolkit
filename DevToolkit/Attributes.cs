using System;

namespace DevToolkit
{
    /// <summary>
    /// Specify how many parameters should be requested in SHVDN.
    /// </summary>
    public class ParametersAttribute : Attribute
    {
        public int Count { get; set; }

        public ParametersAttribute(int count)
        {
            Count = count;
        }
    }

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
