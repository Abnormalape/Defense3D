using BHSSolo.DungeonDefense.Data;
using BHSSolo.DungeonDefense.DungeonRoom;
using BHSSolo.DungeonDefense.ManagerClass;
using BHSSolo.DungeonDefense.State;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace BHSSolo.DungeonDefense.Controller
{
    public class DungeonBuildUI : UIController_, IController
    {
        public override UIManager_ UIManager_ { get; set; }
        public override UI_enum UIType { get; set; } = UI_enum.BuildDungeon;
        public override Canvas myCanvas { get; set; }
        public IManagerClass OwnerManager { get; set; }

        private DataManager_ dataManager_ { get; set; }
        private DungeonConstructManager dungeonConstructManager_ { get; set; }


        [SerializeField]
        private GameObject constructureBluePrintHolder;
        public GameObject ConstructureBluePrintHolder
        {
            get
            {
                if (constructureBluePrintHolder == null)
                    Debug.Log("No Holder Set");
                return constructureBluePrintHolder;
            }
        }

        private CursorManager cursorManager { get; set; }
        private List<GameObject> bluePrints = new(10); //Todo:


        private void Update()
        {
            OnUIUpdate();
        }

        public void ControllerInitializer(IManagerClass ownerManager)
        {
            OwnerManager = ownerManager;
            myCanvas = GetComponent<Canvas>();
            cursorManager = OwnerManager.OwnerManager.CursorManager_;
            dataManager_ = OwnerManager.OwnerManager.DataManager_;
            dungeonConstructManager_ = OwnerManager.OwnerManager.DungeonConstructManager_;

            UIControllerInitializer();
        }

        public void UIControllerInitializer()
        {
            // Todo: Get Data.
            List<RoomBluePrint> tempBluePrints = dataManager_.PlayerData.RoomBluePrints;
            Dictionary<int, RoomBuildData> tempIDRoomData = dataManager_.ID_RoomBuildData;

            foreach (var bluePrint in tempBluePrints)
            {
                if(bluePrint.acquired) //Blueprint acquired.
                {
                    RoomBuildData tempRoomBuildData = tempIDRoomData[bluePrint.RoomID];

                    Debug.Log($"You have Blueprint of {tempRoomBuildData.name}. Its size is {tempRoomBuildData.Width} * {tempRoomBuildData.Depth}");
                }
            }


            //foreach (var bluePrint in tempBluePrintChecker) // 10/10 
            //{
            //    if (bluePrint.Value) //If bluePrint Activated.
            //    {
            //        GameObject tempBluePrint = Instantiate(bluePrintPrefab, constructureBluePrintHolder);
            //        var bluePrintKey = bluePrint.Key;

            //        BlueprintButtonController tempBlueprintButton = tempBluePrint.GetComponent<BlueprintButtonController>();

            //        tempBlueprintButton.BluePrintButtonInitializer(); //Room Name, Type, Size, Resource Needed, Conditions etc...

            //        //Todo: Delegate method below might be move into BluePrintButtonInitializer.
            //        tempBluePrint.GetComponent<Button>().onClick.AddListener(() =>
            //        {
            //            RoomData tempRoomData = tempRoomDatas[bluePrintKey];
            //            string roomName = tempRoomData.Name;
            //            int xSize = tempRoomData.XSize;
            //            int zSize = tempRoomData.ZSize;
            //            RoomType roomType = tempRoomData.RoomType;

            //            SendSelectedRoomData(roomName, xSize, zSize, roomType);
            //        });
            //    }
            //}


            int blueprintscount = constructureBluePrintHolder.transform.childCount; //Todo: Instantiate BluePrint GameObject. Counts of bluepirnts.

            for (int i = 0; i < blueprintscount; i++)
            {
                bluePrints.Add(ConstructureBluePrintHolder.transform.GetChild(i).gameObject);

                string buttonstring;

                if (i < 5) //Todo: Remove
                { buttonstring = $"SamplePassage"; }//Todo: Use Constructure Data to Fill Component
                else
                { buttonstring = $"SampleRoom"; }

                bluePrints[i].GetComponentInChildren<Button>().onClick.AddListener(() => BluePrintClicked(buttonstring)); //Todo: Use Constructure Data to Fill Component
                bluePrints[i].GetComponentInChildren<TextMeshProUGUI>().text = $"{i} : Blue Print"; //Todo: Use Constructure Data to Fill Component
            }
            Close();
        }

        public override void Open()
        {
            myCanvas.enabled = true;
        }

        public override void Close()
        {
            myCanvas.enabled = false;
        }


        public override void FixUI()
        {
        }

        public override void OnUIUpdate()
        {
            if (myCanvas.enabled == false)
                return;
        }

        //==============//

        private int xSize = 3; //Todo: Adjust
        private int zSize = 3; //Todo: Adjust

        private void SendSelectedRoomData(string roomName, int xSize, int zSize, RoomBuildType roomBuildType)
        {
            if (roomBuildType == RoomBuildType.Passage)
            {
                dungeonConstructManager_.PrepareConstructionPassage();
                cursorManager.ChangeManagerState(CursorState.OnManage_Grid);
            }
            else if (roomBuildType == RoomBuildType.Room)
            {
                dungeonConstructManager_.PrepareConstructionRoom();
                cursorManager.ChangeManagerState(CursorState.OnManage_Grid);
            }

            StartCoroutine(QuitSelectionState());
        }

        private IEnumerator QuitSelectionState()
        {
            yield return Input.GetMouseButtonDown(2);
            //Quit Selection State
        }

        private void BluePrintClicked(string ButtonString) //Todo: Adjust
        {
            //Block Action
            if ((cursorManager.CurrentState as ICursorState).CursorState != CursorState.OnManage_Idle)
                return;

            Debug.Log($"{ButtonString} Clicked.");

            if (ButtonString == "SamplePassage") { xSize = 1; zSize = 1; }
            else if (ButtonString == "SampleRoom") { xSize = 3; zSize = 3; }

            cursorManager.SetHoldingRoomData(ButtonString, xSize, zSize);
            cursorManager.ChangeManagerState(CursorState.OnManage_Grid);
        }
    }

    public enum RoomBuildType //Todo: Move to other namespace.
    {
        Passage,
        Room,
    }
}