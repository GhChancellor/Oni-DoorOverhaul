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

        private bool _isReplacement;

        private PneumaticTrapDoorManager pneumaticTrapDoor =
            new PneumaticTrapDoorManager();

        public void SetReplacement(bool isReplacement)
        {
            _isReplacement = isReplacement;
            Debug.Log("PneumaticTrapDoor SetReplacement - " + _isReplacement);

        }

        public override BuildingDef CreateBuildingDef()
        {

            float[] constructionMass;
            float constructionTime;

            Debug.Log("PneumaticTrapDoor - _isReplacement before if - " + _isReplacement);

            if (_isReplacement)
            {
                (constructionMass, constructionTime) = Replace();
                Debug.Log("PneumaticTrapDoor Replace - " + _isReplacement);
            }
            else
            {
                (constructionMass, constructionTime) = Create();
                Debug.Log("PneumaticTrapDoor Create - " + _isReplacement);
            }

            Debug.Log($"PneumaticTrapDoor - constructionTime {constructionTime}");


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
                construction_time: constructionTime,
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

            Debug.Log($"PneumaticTrapDoor - Final constructionTime in BuildingDef: {buildingDef.ConstructionTime}");
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
            go.AddOrGet<MoveButton>();
            ((KAnimControllerBase)go.GetComponent<KBatchedAnimController>()).initialAnim = "closed";
            go.AddOrGet<ZoneTile>();
            go.AddOrGet<KBoxCollider2D>();
            Prioritizable.AddRef(go);
            Object.DestroyImmediate((Object)go.GetComponent<BuildingEnabledButton>());
        }

        public (float[], float) Create()
        {

            float[] constructionMass;
            float constructionTime;

            constructionMass = TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER1;
            constructionTime = 10f; // 5 seconds

            Debug.Log($"Create - constructionMass: {string.Join(",", constructionMass)}, constructionTime: {constructionTime}");

            return (constructionMass, constructionTime);
        }

        public (float[], float) Replace()
        {
            float[] constructionMass;
            float constructionTime;

            constructionMass = TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER_TINY;
            constructionTime = 1f; // 0 seconds

            Debug.Log($"Replace - constructionMass: {string.Join(",", constructionMass)}, constructionTime: {constructionTime}");

            return (constructionMass, constructionTime);
        }
    }
}