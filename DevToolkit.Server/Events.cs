using CitizenFX.Core;
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

        [EventHandler("devtoolkit:changeModel")]
        public void ChangeModel([FromSource]Player player, string model)
        {
            if (API.IsPlayerAceAllowed(player.Handle, "devtoolkit.changemodel"))
            {
                player.TriggerEvent("devtoolkit:changeModel", model);
            }
        }

        [EventHandler("devtoolkit:giveWeapons")]
        public void GiveWeapons([FromSource]Player player)
        {
            if (API.IsPlayerAceAllowed(player.Handle, "devtoolkit.giveweapons"))
            {
                player.TriggerEvent("devtoolkit:giveWeapons");
            }
        }

        [EventHandler("devtoolkit:giveWeapon")]
        public void GiveWeapon([FromSource]Player player, string weapon)
        {
            if (API.IsPlayerAceAllowed(player.Handle, "devtoolkit.giveweapons"))
            {
                player.TriggerEvent("devtoolkit:giveWeapon", weapon);
            }
        }

        [EventHandler("devtoolkit:fixVehicle")]
        public void FixVehicle([FromSource]Player player)
        {
            if (API.IsPlayerAceAllowed(player.Handle, "devtoolkit.fixvehicle"))
            {
                player.TriggerEvent("devtoolkit:fixVehicle");
            }
        }

        [EventHandler("devtoolkit:playSound")]
        public void PlaySound([FromSource]Player player, string sound, string bank)
        {
            if (API.IsPlayerAceAllowed(player.Handle, "devtoolkit.playsound"))
            {
                player.TriggerEvent("devtoolkit:playSound", sound, bank);
            }
        }

        [EventHandler("devtoolkit:loadIPL")]
        public void LoadIPL([FromSource]Player player, string ipl)
        {
            if (API.IsPlayerAceAllowed(player.Handle, "devtoolkit.ipl"))
            {
                player.TriggerEvent("devtoolkit:loadIPL", ipl);
            }
        }

        [EventHandler("devtoolkit:unloadIPL")]
        public void UnloadIPL([FromSource]Player player, string ipl)
        {
            if (API.IsPlayerAceAllowed(player.Handle, "devtoolkit.ipl"))
            {
                player.TriggerEvent("devtoolkit:unloadIPL", ipl);
            }
        }
    }
}
