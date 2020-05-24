using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace DevToolkit.Server
{
    public class DevToolkit : BaseScript 
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
