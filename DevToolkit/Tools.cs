#if FIVEM
using CitizenFX.Core;
using CitizenFX.Core.Native;
using System.Data;
#elif SINGLEPLAYER
using GTA;
using GTA.Math;
using GTA.Native;
using GTA.UI;
using System.IO;
#endif
using System;
using System.Threading.Tasks;

namespace DevToolkit
{
    public enum CoordType
    {
        CSharp,
        Lua
    }

    /// <summary>
    /// Tools for quick access to GTA V data in both FiveM and SHVDN.
    /// </summary>
    public static class Tools
    {
        /// <summary>
        /// Gets the coordinates of the local player ped or vehicle.
        /// </summary>
        /// <returns>A Vector3 with the coordinates of the player or player vehicle.</returns>
        public static Vector3 PlayerCoords
        {
            get
            {
                if (Game.Player.Character.CurrentVehicle != null)
                {
                    return Game.Player.Character.CurrentVehicle.Position;
                }
                else
                {
                    return Game.Player.Character.Position;
                }
            }
            set
            {
                if (Game.Player.Character.CurrentVehicle != null)
                {
                    Game.Player.Character.CurrentVehicle.Position = value;
                }
                else
                {
                    Game.Player.Character.Position = value;
                }
            }
        }
        /// <summary>
        /// The heading of the player ped or player vehicle.
        /// </summary>
        public static float Heading
        {
            get
            {
                if (Game.Player.Character.CurrentVehicle != null)
                {
                    return Game.Player.Character.CurrentVehicle.Heading;
                }
                else
                {
                    return Game.Player.Character.Heading;
                }
            }
            set
            {
                if (Game.Player.Character.CurrentVehicle != null)
                {
                    Game.Player.Character.CurrentVehicle.Heading = value;
                }
                else
                {
                    Game.Player.Character.Heading = value;
                }
            }
        }

        /// <summary>
        /// Shows a message for the respective platform.
        /// On FiveM, is printed on the console. For SHVDN, a notification is shown.
        /// </summary>
        /// <param name="message">The message to show.</param>
        public static void ShowMessage(string message)
        {
#if SINGLEPLAYER
            Notification.Show(message);
#elif FIVEM
            Debug.WriteLine(message);
#endif
        }

        /// <summary>
        /// Saves the player coordinates.
        /// </summary>
        public static void SaveCoords(CoordType mode)
        {
            // Select the correct prefix and sufix
            string prefix = "";
            string sufix = "";
            switch (mode)
            {
                case CoordType.CSharp:
                    prefix = "[C#] new Vector3";
                    sufix = ";";
                    break;
                case CoordType.Lua:
                    prefix = "[Lua] vector3";
                    break;
            }

            // Get the coordenates and show them in the correct format 
            Vector3 coords = PlayerCoords;
            string format = $"{prefix}({coords.X}, {coords.Y}, {coords.Z}){sufix}";
            ShowMessage(format);
#if SINGLEPLAYER
            // On SP, manually save it into a text file
            File.AppendAllText("scripts\\DevToolkit.Coords.txt", format + Environment.NewLine);
#endif
        }

        /// <summary>
        /// Spawns a vehicle and places the player on the driver seat.
        /// </summary>
        /// <param name="model">The vehicle model to use.</param>
        /// <returns>The vehicle created, null otherwise.</returns>
        public static async Task SpawnVehicle(string modelName)
        {
            // If the player is using a vehicle, delete it
            Game.Player.Character.CurrentVehicle?.Delete();

            // Create the Model object
            Model model = new Model(modelName);
            // If the model is not a vehicle, notify the user and return
            if (!model.IsVehicle)
            {
                ShowMessage("The model specified does not exists or is not a vehicle");
                return;
            }

            // Try to create the vehicle
#if SINGLEPLAYER
            Vehicle vehicle = World.CreateVehicle(model, PlayerCoords, Heading);
#elif FIVEM
            Vehicle vehicle = await World.CreateVehicle(model, PlayerCoords, Heading);
#endif

            // If the vehicle is invalid, return
            if (vehicle == null)
            {
                ShowMessage("Unable to create the vehicle");
                return;
            }

            // Otherwise, set the player in the driver seat
#if SINGLEPLAYER
            Function.Call(Hash.SET_PED_INTO_VEHICLE, Game.Player.Character.Handle, vehicle.Handle, -1);
#elif FIVEM
            API.SetPedIntoVehicle(Game.Player.Character.Handle, vehicle.Handle, -1);
#endif
            // And mark the vehicle as no longer needed
            vehicle.MarkAsNoLongerNeeded();
        }

        /// <summary>
        /// Changes the player model to the one specified.
        /// </summary>
        /// <param name="model">The model to change.</param>
        public static void ChangePlayerModel(string modelName)
        {
            // Create the Model object
            Model model = new Model(modelName);
            // If the model is not a ped, notify the user and return
            if (!model.IsPed)
            {
                ShowMessage("The model specified does not exists or is not a ped");
                return;
            }

            // Otherwise, set it for the player
            Game.Player.ChangeModel(model);
        }

        /// <summary>
        /// Gives all of the weapons to the current player.
        /// </summary>
        public static void GiveAllWeapons()
        {
            // Iterate over the weapons and add all of them
            // Thanks me from 2018 for this!
            foreach (WeaponHash weapon in Enum.GetValues(typeof(WeaponHash)))
            {
                Game.Player.Character.Weapons.Give(weapon, 9999, false, true);
            }
        }

        /// <summary>
        /// Gives the specified weapon to the player.
        /// </summary>
        /// <param name="weapon">The weapon to give</param>
        public static void GiveWeapon(string weapon)
        {
            // Convert the weapon name to uppercase
            weapon = weapon.ToUpperInvariant();

            // If it does not starts with the prefix, add it
            if (!weapon.StartsWith("WEAPON_"))
            {
                weapon = "WEAPON_" + weapon;
            }

            // Convert it to a hash
            Hash hash = (Hash)Game.GenerateHash(weapon);
            // And give the weapon to the player
            Game.Player.Character.Weapons.Give((WeaponHash)hash, 9999, true, true);
        }
    }
}
