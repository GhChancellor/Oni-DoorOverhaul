using TUNING;
using UnityEngine;

namespace Door_Overhaul
{
    internal class PneumaticTrapDoorReplace : IBuildingConfig
    {
        private static string buildingId = "PneumaticTrapDoorReplace";
        private static string categoryMenu = "Base";
        private static string subCategoryID = "Door";
        private static string tech = "none";

        private float constructionTime; // = 4f;
        private float[] constructionMass; // = BUILDINGS.CONSTRUCTION_MASS_KG.TIER1;

        private string anim = "tiny_door_internal_kanim";
        private int width = 1;
        private int height = 1;
        private int hitPoints = 30;
        private string[] constructionMaterials = MATERIALS.ALL_METALS;
        private float meltingPoint = 1600f;
        private string[] allMetals = MATERIALS.ALL_METALS;
        private BuildLocationRule buildLocationRule = BuildLocationRule.Tile;
        private EffectorValues decor = BUILDINGS.DECOR.NONE;
        private EffectorValues noise = NOISE_POLLUTION.NONE;
        private EffectorValues noisePollution = NOISE_POLLUTION.NOISY.TIER2;
        private float temperatureModificationMassScale = 1f;


        public PneumaticTrapDoorReplace():base()
        {
            var Construction = Replace();

            constructionMass = Construction.constructionMass;
            constructionTime = Construction.constructionTime;
        }
        public override BuildingDef CreateBuildingDef()
        {
            BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(
                id: buildingId,
                width: width,
                height: height,
                anim: anim,
                hitpoints: hitPoints,
                construction_time: constructionTime,
                construction_mass: constructionMass,
                construction_materials: allMetals,
                melting_point: meltingPoint,
                build_location_rule: buildLocationRule,
                decor: decor,
                noise: noise,
                temperature_modification_mass_scale:
                    temperatureModificationMassScale
                );

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
            SoundEventVolumeCache.instance.AddVolume("door_internal_kanim", "Open_DoorInternal", noisePollution);
            SoundEventVolumeCache.instance.AddVolume("door_internal_kanim", "Close_DoorInternal", noisePollution);

            return buildingDef;
        }

        public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
        {
            Door door = go.AddOrGet<Door>();
            door.hasComplexUserControls = true;
            door.unpoweredAnimSpeed = 1f;
            door.doorType = Door.DoorType.ManualPressure;
            go.AddOrGet<ZoneTile>();
            go.AddOrGet<AccessControl>();
            go.AddOrGet<KBoxCollider2D>();
            go.AddOrGet<MoveButton>();
            Prioritizable.AddRef(go);
            go.AddOrGet<CopyBuildingSettings>().copyGroupTag = GameTags.Door;
            go.AddOrGet<Workable>().workTime = 5f;
            UnityEngine.Object.DestroyImmediate(go.GetComponent<BuildingEnabledButton>());
        }

        public override void DoPostConfigureComplete(GameObject go)
        {
            go.GetComponent<AccessControl>().controlEnabled = true;
            go.GetComponent<KBatchedAnimController>().initialAnim = "closed";
        }

        // <summary>
        // Change recipe value constructionMass and constructionTime 
        // for the construction
        // </summary>
        // <returns>float[] constructionMass and float constructionTime).</returns>
        private (float[] constructionMass, float constructionTime) Replace()
        {
            PneumaticTrapDoorManager pneumaticTrapDoorManager =
                new PneumaticTrapDoorManager();

            return pneumaticTrapDoorManager.Replace();
        }

        // Getter per la proprietà ID (non statico)
        public static string GetID()
        {
            return buildingId;
        }

        // Getter per le altre proprietà statiche
        public static string GetCategoryMenu()
        {
            return categoryMenu;
        }

        public static string GetSubCategoryID()
        {
            return subCategoryID;
        }

        public static string GetTech()
        {
            return tech;
        }

    }
}

// ------------------------------------------------------

//public PneumaticTrapDoor(float constructionTime, float[] constructionMass, string anim, int width, int height, int hitPoints, string[] constructionMaterials, float meltingPoint, string[] allMetals, BuildLocationRule buildLocationRule_, EffectorValues decor, EffectorValues noise, EffectorValues noisePollution, float temperatureModificationMassScale)
//{
//    ConstructionTime = constructionTime;
//    ConstructionMass = constructionMass;
//    Anim = anim;
//    Width = width;
//    Height = height;
//    HitPoints = hitPoints;
//    ConstructionMaterials = constructionMaterials;
//    MeltingPoint = meltingPoint;
//    AllMetals = allMetals;
//    BuildLocationRule_ = buildLocationRule_;
//    Decor = decor;
//    Noise = noise;
//    NoisePollution = noisePollution;
//    TemperatureModificationMassScale = temperatureModificationMassScale;
//}

// ------------------------------------------------------