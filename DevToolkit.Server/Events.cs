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

        [EventHandler("devtoolkit:spawnVehicle")]
        public void SetPosition([FromSource]Player player, string model)
        {
            // If the player has permission to spawn a vehicle, do it
            if (API.IsPlayerAceAllowed(player.Handle, "devtoolkit.spawnvehicle"))
            {
                player.TriggerEvent("devtoolkit:spawnVehicle", model);
            }
        }

        [EventHandler("devtoolkit:deleteVehicle")]
        public void DeleteVehicle([FromSource]Player player)
        {
            if (API.IsPlayerAceAllowed(player.Handle, "devtoolkit.deletevehicle"))
            {
                player.TriggerEvent("devtoolkit:deleteVehicle");
            }
        }
    }
}
