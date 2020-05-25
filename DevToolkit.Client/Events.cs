using CitizenFX.Core;

namespace DevToolkit.Client
{
    /// <summary>
    /// Deals with the network events coming from the server.
    /// </summary>
    public class Events : BaseScript
    {
        [EventHandler("devtoolkit:setPosition")]
        public void SetPosition(float x, float y, float z)
        {
            Tools.PlayerCoords = new Vector3(x, y, z);
        }

        [EventHandler("devtoolkit:spawnVehicle")]
        public void SpawnVehicle(string model)
        {
            Tools.SpawnVehicle(model);
        }
    }
}
