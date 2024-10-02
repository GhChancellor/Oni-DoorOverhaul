using TUNING;
using UnityEngine;

namespace Door_Overhaul
{
    /// <summary>
    /// Represents a pneumatic trap door in the game.
    /// </summary>
    public class PneumaticTrapDoor : DoorConfig
    {
        private const string buildingID = "PneumaticTrapDoor";
        private const string categoryMenu = "Base";
        private const string subCategoryID = "Door";
        private const string techID = "none";

        /// <summary>
        /// Creates the building definition for the pneumatic trap door.
        /// </summary>
        /// <returns>A BuildingDef object representing the trap door's properties.</returns>
        public override BuildingDef CreateBuildingDef()
        {
            /* Change construction time and mass */
            var Construction = InitiateTrapDoorBuild();

            BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(
                id: buildingID,
                width: 1,
                height: 1,
                anim: "trap_door_external_kanim",
                hitpoints: 30,
                construction_time: Construction.constructionTime,
                construction_mass: Construction.constructionMass,
                construction_materials: MATERIALS.ALL_METALS,
                melting_point: 1600f,
                build_location_rule: BuildLocationRule.Tile,
                decor: BUILDINGS.DECOR.NONE,
                noise: NOISE_POLLUTION.NONE,
                temperature_modification_mass_scale: 1f
            );

            /* Set additional properties for the building definition */
            buildingDef.Overheatable = true;
            buildingDef.Floodable = false;
            buildingDef.Entombable = false;
            buildingDef.IsFoundation = true;
            buildingDef.TileLayer = ObjectLayer.FoundationTile;
            buildingDef.AudioCategory = "Metal";
            buildingDef.PermittedRotations = PermittedRotations.R90;
            buildingDef.SceneLayer = Grid.SceneLayer.TileMain;
            buildingDef.ForegroundLayer = Grid.SceneLayer.InteriorWall;
            buildingDef.LogicInputPorts = DoorConfig.CreateSingleInputPortList(new CellOffset(0, 0));

            /* Add sound events */
            SoundEventVolumeCache.instance.AddVolume("door_internal_kanim", "Open_DoorInternal", NOISE_POLLUTION.NOISY.TIER2);
            SoundEventVolumeCache.instance.AddVolume("door_internal_kanim", "Close_DoorInternal", NOISE_POLLUTION.NOISY.TIER2);

            return buildingDef;
        }

        /// <summary>
        /// Performs post-configuration setup for the pneumatic trap door game object.
        /// </summary>
        /// <param name="go">The GameObject to configure.</param>
        public override void DoPostConfigureComplete(GameObject go)
        {
            /* Configure the door component */
            Door door = go.AddOrGet<Door>();
            door.unpoweredAnimSpeed = 1f;
            door.doorType = Door.DoorType.Internal;
            door.doorOpeningSoundEventName = "Open_DoorInternal";
            door.doorClosingSoundEventName = "Close_DoorInternal";

            MoveButton.BuildingID = buildingID;
            
            /* Add new button */
            go.AddOrGet<MoveButton>();

            /* Add additional components and configure them */
            go.AddOrGet<AccessControl>().controlEnabled = true;
            go.AddOrGet<CopyBuildingSettings>().copyGroupTag = GameTags.Door;
            go.AddOrGet<Workable>().workTime = 3f;
            go.GetComponent<KBatchedAnimController>().initialAnim = "closed";
            go.AddOrGet<ZoneTile>();
            go.AddOrGet<KBoxCollider2D>();
            Prioritizable.AddRef(go);

            /* Remove the BuildingEnabledButton component */
            UnityEngine.Object.DestroyImmediate(go.GetComponent<BuildingEnabledButton>());
        }

        /// <summary>
        /// Initializes the trap door build by changing recipe values for construction mass and time.
        /// </summary>
        /// <returns>A tuple containing the construction mass array and construction time.</returns>
        private (float[] constructionMass, float constructionTime) InitiateTrapDoorBuild()
        {
            PneumaticTrapDoorManager pneumaticTrapDoorManager =
                new PneumaticTrapDoorManager();

            return pneumaticTrapDoorManager.Create();
        }

        /// <summary>
        /// Gets the unique identifier for this building.
        /// </summary>
        /// <returns>The building's ID.</returns>
        public static string GetBuildingID()
        {
            return buildingID;
        }

        /// <summary>
        /// Gets the category menu where this building will be displayed in the game.
        /// </summary>
        /// <returns>The category menu name.</returns>
        public static string GetCategoryMenu()
        {
            return categoryMenu;
        }

        /// <summary>
        /// Gets the subcategory ID for this building within the build menu.
        /// </summary>
        /// <returns>The subcategory ID.</returns>
        public static string GetSubCategoryID()
        {
            return subCategoryID;
        }

        /// <summary>
        /// Gets the technology required to unlock this building in the tech tree.
        /// </summary>
        /// <returns>The technology name.</returns>
        public static string GetTechID()
        {
            return techID;
        }
    }
}