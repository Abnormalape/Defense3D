using BHSSolo.DungeonDefense.ManagerClass;
using BHSSolo.DungeonDefense.State;
using System.Collections.Generic;
using TMPro;
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

            UIControllerInitializer();
        }

        public void UIControllerInitializer()
        {
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

        private void BluePrintClicked(string ButtonString) //Todo: Adjust
        {
            //Block Reaction
            if ((cursorManager.CurrentState as ICursorState).CursorState != CursorState.OnManage_Idle)
                return;

            Debug.Log($"{ButtonString} Clicked.");

            if (ButtonString == "SamplePassage") { xSize = 1; zSize = 1; }
            else if (ButtonString == "SampleRoom") { xSize = 3; zSize = 3; }

            cursorManager.SetHoldingRoomData(ButtonString, xSize, zSize);
            cursorManager.ChangeManagerState(CursorState.OnManage_Grid);
        }
    }
}