﻿using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace DevToolkit.Server
{
    /// <summary>
    /// Deals with the ACL Confirmation for dangerous client events.
    /// </summary>
    public class Events : BaseScript 
    {
        [EventHandler("devtoolkit:setPosition")]
        public void SetPosition([FromSource]Player player, float x, float y, float z)
        {
            // If the player has permission to set the position of it, do it
            if (API.IsPlayerAceAllowed(player.Handle, "devtoolkit.setposition"))
            {
                player.TriggerEvent("devtoolkit:setPosition", x, y, z);
            }
        }
    }
}