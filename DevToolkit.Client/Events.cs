using CitizenFX.Core;
using CitizenFX.Core.Native;

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

        [EventHandler("devtoolkit:deleteVehicle")]
        public void DeleteVehicle()
        {
            Game.Player.Character.CurrentVehicle?.Delete();
        }

        [EventHandler("devtoolkit:changeModel")]
        public void ChangeModel(string model)
        {
            Tools.ChangePlayerModel(model);
        }

        [EventHandler("devtoolkit:giveWeapons")]
        public void GiveWeapons()
        {
            Tools.GiveAllWeapons();
        }

        [EventHandler("devtoolkit:giveWeapon")]
        public void GiveWeapon(string weapon)
        {
            Tools.GiveWeapon(weapon);
        }

        [EventHandler("devtoolkit:fixVehicle")]
        public void FixVehicle()
        {
            Game.Player.Character.CurrentVehicle?.Repair();
        }

        [EventHandler("devtoolkit:playSound")]
        public void PlaySound(string sound, string bank)
        {
            Tools.PlaySound(sound, bank);
        }

        [EventHandler("devtoolkit:loadIPL")]
        public void LoadIPL(string ipl)
        {
            API.RequestIpl(ipl);
        }

        [EventHandler("devtoolkit:unloadIPL")]
        public void UnloadIPL(string ipl)
        {
            API.RemoveIpl(ipl);
        }
    }
}
