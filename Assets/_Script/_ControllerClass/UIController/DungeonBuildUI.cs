using BHSSolo.DungeonDefense.Data;
using BHSSolo.DungeonDefense.ManagerClass;
using BHSSolo.DungeonDefense.State;
using System;
using System.Collections;
using System.Collections.Generic;
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
        private GameStateManager_ gameStateManager_ { get; set; }


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

        [SerializeField]
        private GameObject bluePrintButtonPrefab;

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
            gameStateManager_ = OwnerManager.OwnerManager.GameStateManager_;
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
                if (bluePrint.acquired) //Blueprint acquired.
                {
                    RoomBuildData tempRoomBuildData = tempIDRoomData[bluePrint.RoomID];

                    GameObject tempButton = Instantiate(bluePrintButtonPrefab, constructureBluePrintHolder.transform);
                    Button blueprintButton = tempButton.GetComponentInChildren<Button>();

                    blueprintButton.onClick.AddListener(() =>
                    {
                        int roomId = tempRoomBuildData.RoomID;
                        string roomName = tempRoomBuildData.name;
                        int roomDepth = tempRoomBuildData.Depth;
                        int roomWidth = tempRoomBuildData.Width;
                        RoomBuildType buildType;
                        Enum.TryParse(tempRoomBuildData.roomType, out buildType);
                        Requirement tempRequire = tempRoomBuildData.Requirements;

                        //Todo: Check Requirement To Build. If your gold, workforce or etc is under Requirement value, can't build.

                        SendSelectedRoomData(roomId, roomName, buildType, roomDepth, roomWidth, tempRequire);
                        BluePrintClicked();
                    });
                }
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

        private void SendSelectedRoomData(int roomId, string roomName,
            RoomBuildType buildType, int roomWidth, int roomDepth, Requirement tempRequire)
        {
            if ((cursorManager.CurrentState as ICursorState).CursorState != CursorState.OnManage_Idle)
                return;

            dungeonConstructManager_.ConstructionProgress.SetHoldingBuildData(buildType, roomName, roomId, roomWidth, roomDepth, tempRequire);
        }

        private void BluePrintClicked()
        {
            //Action Blocker
            if ((cursorManager.CurrentState as ICursorState).CursorState != CursorState.OnManage_Idle)
                return;

            StopCoroutine(QuitGridBuildingState());
            Debug.Log("BluePrintClicked");
            cursorManager.ChangeManagerState(CursorState.OnManage_Grid);
            StartCoroutine(QuitGridBuildingState());
        }

        private IEnumerator QuitGridBuildingState()
        {
            while (!Input.GetMouseButtonUp(1) || ((cursorManager.CurrentState as ICursorState).CursorState == CursorState.OnManage_Idle))
            {

                yield return null;
            }

            if ((cursorManager.CurrentState as ICursorState).CursorState == CursorState.OnManage_Idle)
            {
                yield break;
            }

            dungeonConstructManager_.ConstructionProgress.ResetHoldingBuildData();
            dungeonConstructManager_.ConstructionProgress.ResetTempRoomData();
            gameStateManager_.ChangeManagerState(GameState.Dungeon_ConstructionState);
        }
    }

    public enum RoomBuildType //Todo: Move to other namespace.
    {
        None,
        Passage,
        Room,
    }
}