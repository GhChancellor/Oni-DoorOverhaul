//using UnityEngine;
//using TUNING;
//using STRINGS;

//namespace Door_Overhaul
//{
//    public class ButtonX2 : KMonoBehaviour
//    {
//#pragma warning disable CS0649
//        [MyCmpGet]
//        private Deconstructable deconstructable;

//        [MyCmpGet]
//        private PrimaryElement primaryElement;
//#pragma warning restore CS0649

//        private DoorMenuManager doorMenuManager;

//        protected override void OnPrefabInit()
//        {
//            base.OnPrefabInit();
//            this.Subscribe((int)GameHashes.RefreshUserMenu, new System.Action<object>(OnRefreshUserMenu));
//        }

//        private void OnRefreshUserMenu(object data)
//        {
//            if (doorMenuManager == null)
//            {
//                doorMenuManager = new DoorMenuManager(this.gameObject, deconstructable, primaryElement);
//            }
//            doorMenuManager.AddRemoveDoorButton();
//        }
//    }
//}
