using static STRINGS.UI;

namespace Door_Overhaul
{
    internal class STRINGS
    {
        public class BUILDINGS
        {
            public class PREFABS
            {
                public class PNEUMATICTRAPDOOR
                {
                    public static LocString NAME = FormatAsLink("Pneumatic Trap Door", PneumaticTrapDoor.GetBuildingID());
                    public static LocString DESC = "Pneumatic Trap Door, designed for tight spaces and easy access.";
                    public static LocString EFFECT = "Encloses areas without blocking " +
                        FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " or " +
                        FormatAsLink("Gas", "ELEMENTS_GAS") + " flow.\n\nWild " +
                        FormatAsLink("Critters", "CREATURES") +
                        " cannot pass through doors.";
                }

                public class PNEUMATICTRAPDOORREPLACE
                {
                }

                public class MOVEDOOR
                {
                    public static LocString NAME = "Move door";
                    public static LocString TOOLTIP = "Move this door";
                }
            }
        }
    }
}

