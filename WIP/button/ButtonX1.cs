//using System;
//using UnityEngine;

///* https://github.com/Sgt-Imalas/Sgt_Imalas-Oni-Mods/blob/284b32141b596475adf4d03833417ddb6b43ef4f/ConveyorTiles/ConveyorTileSM.cs */

//namespace Door_Overhaul
//{
//    internal class ButtonX1 : StateMachineComponent<ButtonX1.StatesInstance>
//    {

//        private static readonly EventSystem.IntraObjectHandler<ButtonX1>
//            OnRefreshUserMenuDelegate = new EventSystem.IntraObjectHandler<ButtonX1>(
//                (System.Action<ButtonX1, object>)
//                ((component, data) => component.OnRefreshUserMenu(data)));


//        protected override void OnPrefabInit() => 
//            this.Subscribe<ButtonX1>( (int) GameHashes.RefreshUserMenu, OnRefreshUserMenuDelegate);

//        private void OnRefreshUserMenu(object data)
//            => Game.Instance.userMenu.AddButton(
//                go: this.gameObject,
//                button: new KIconButtonMenu.ButtonInfo(
//                    iconName: "action_mirror",
//                    text: STRINGS.BUILDINGS.PREFABS.MOVEDOOR.NAME,
//                    on_click: new System.Action(() => this.Reverse(this.gameObject)), // new System.Action(this.Reverse("15")  ),
//                    shortcutKey: Action.BuildingUtility1,
//                    tooltipText: STRINGS.BUILDINGS.PREFABS.MOVEDOOR.TOOLTIP
//                    )
//            );

//        public void Reverse(GameObject gameObject)
//        {
//            Util.KDestroyGameObject(this.gameObject);
//        }

//        public class StatesInstance : GameStateMachine<States, StatesInstance, ButtonX1>.GameInstance
//        {
//            public StatesInstance(ButtonX1 master) : base(master)
//            {
//                Console.WriteLine("StatesInstance");
//            }
//        }

//        public class States : GameStateMachine<ButtonX1.States, ButtonX1.StatesInstance, ButtonX1, object>
//        {

//        }
//    }
//}



