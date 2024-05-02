using TUNING;
using UnityEngine;

namespace Door_Overhaul
{
    internal class PneumaticTrapDoor : IBuildingConfig
    {
        /* Name of item */
        public const string ID = "PneumaticTrapDoor";

        /* Which build menu to add to */
        public const string categoryMenu = "Base";

        /* Which item in build menu to add after */
        public const string subCategoryID = "Door";

        /* Which tech tree entry to add to, "none" if no research is requried. */
        public const string tech = "none";

        public override BuildingDef CreateBuildingDef()
        {
            float[] constructionMass = TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER1;
            string[] allMetals = MATERIALS.ALL_METALS;

            EffectorValues decor = TUNING.BUILDINGS.DECOR.NONE;
            EffectorValues noise = TUNING.NOISE_POLLUTION.NONE;
            EffectorValues noisePollution = TUNING.NOISE_POLLUTION.NOISY.TIER2;

            BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(
                id: PneumaticTrapDoor.ID,
                width: 1,
                height: 1,
                anim: "tiny_door_internal_kanim",
                hitpoints: 30,
                construction_time: 5f,
                construction_mass: constructionMass,
                construction_materials: allMetals,
                melting_point: 1600f,
                build_location_rule: BuildLocationRule.Tile,
                decor: decor,
                noise: noise,
                temperature_modification_mass_scale: 1f);

            buildingDef.Entombable = true;
            buildingDef.Floodable = false;
            buildingDef.IsFoundation = false;
            buildingDef.AudioCategory = "Metal";
            buildingDef.PermittedRotations = PermittedRotations.R90;
            buildingDef.ForegroundLayer = Grid.SceneLayer.TileMain;
            buildingDef.LogicInputPorts = DoorConfig.CreateSingleInputPortList(new CellOffset(0, 0));
            SoundEventVolumeCache.instance.AddVolume("door_internal_kanim", "Open_DoorInternal", noisePollution);
            SoundEventVolumeCache.instance.AddVolume("door_internal_kanim", "Close_DoorInternal", noisePollution);
            return buildingDef;
        }

        public override void DoPostConfigureComplete(GameObject go)
        {
            Door door = go.AddOrGet<Door>();
            door.unpoweredAnimSpeed = 1f;
            door.doorType = Door.DoorType.Internal;
            door.doorOpeningSoundEventName = "Open_DoorInternal";
            door.doorClosingSoundEventName = "Close_DoorInternal";
            go.AddOrGet<AccessControl>().controlEnabled = true;
            go.AddOrGet<CopyBuildingSettings>().copyGroupTag = GameTags.Door;
            go.AddOrGet<Workable>().workTime = 3f;
            ((KAnimControllerBase)go.GetComponent<KBatchedAnimController>()).initialAnim = "closed";
            go.AddOrGet<ZoneTile>();
            go.AddOrGet<KBoxCollider2D>();
            Prioritizable.AddRef(go);
            Object.DestroyImmediate((Object)go.GetComponent<BuildingEnabledButton>());
        }
    }
}
 
