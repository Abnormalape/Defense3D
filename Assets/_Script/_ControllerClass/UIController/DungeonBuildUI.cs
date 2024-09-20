using BHSSolo.DungeonDefense.ManagerClass;
using JetBrains.Annotations;
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
        private GameObject ConstructureBluePrintPrefab; //Todo: Prefab

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
        private DungeonConstructManager dungeonConstructManager;

        private List<GameObject> bluePrints = new(10); //Todo:


        private void Update()
        {
            OnUIUpdate();
        }

        public void ControllerInitializer(IManagerClass ownerManager)
        {
            OwnerManager = ownerManager;
            myCanvas = GetComponent<Canvas>();
            this.cursorManager = OwnerManager.OwnerManager.CursorManager_;
            this.dungeonConstructManager = OwnerManager.OwnerManager.DungeonConstructManager_;

            int blueprintscount = constructureBluePrintHolder.transform.childCount; //Todo: Instantiate BluePrint GameObject. Counts of bluepirnts.
            for (int i = 0; i < blueprintscount; i++)
            {
                bluePrints.Add(ConstructureBluePrintHolder.transform.GetChild(i).gameObject);

                string buttonstring = $"{i} Button"; //Todo: Use Constructure Data to Fill Component
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

        [SerializeField] private GameObject tempPrefab; //Todo: Remove
        private bool isAttached = false; //Todo: Remove
        private float passedTime = 0f; //Todo: Remove
        private GameObject summoned = null; //Todo: Remove
        private int xSize = 3; //Todo: Adjust
        private int zSize = 3; //Todo: Adjust
        private void BluePrintClicked(string ButtonString) //Todo: temp Method
        {
            //if(isAttached) 
            //    return;

            Debug.Log($"{ButtonString} Clicked.");

            //Find Prefab to Instantiate with string ButtonString.
            //Like => Resources.Load($"Prefab/Rooms/{ButtonString}") as GameObject

            //isAttached = true;

            GameObject tempConstructure = Instantiate(tempPrefab);

            dungeonConstructManager.ConstructurePlaceHolder = tempConstructure;
            cursorManager.AttachGameObjectToCursor(CursorType.GridCursor, tempConstructure, ButtonString);
        }
    }
}