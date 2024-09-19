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

        private CursorManager cursorManager;
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


            if (isAttached == true) //Todo: Remove
            {
                passedTime += Time.deltaTime;
                if (passedTime > 1f)
                {
                    passedTime = 0f;

                    int tempXSize = 0;
                    int tempZSize = 0;

                    if (xSize % 2 == 1)
                        tempXSize = (xSize - 1) / 2;
                    if (zSize % 2 == 1)
                        tempZSize = (zSize - 1) / 2;

                    Vector3 tempPosition = summoned.transform.position;

                    for (int ix = -tempXSize; ix < tempXSize + 1; ix++)
                    {
                        for (int iz = -tempZSize; iz < tempZSize + 1; iz++)
                        {
                            if (dungeonConstructManager.GridDatas.ContainsKey(new Vector3(tempPosition.x + (ix * 5f), 0.05f, tempPosition.z + (iz * 5f))))
                                Debug.Log("Vector3 Found");
                            //Debug.Log(new Vector3(tempPosition.x + (ix * 5f), tempPosition.y, tempPosition.z + (iz * 5f)));
                        }
                    }
                }
            }
        }

        [SerializeField] private GameObject tempPrefab; //Todo: Remove
        private bool isAttached = false; //Todo: Remove
        private float passedTime = 0f; //Todo: Remove
        private GameObject summoned = null; //Todo: Remove
        private int xSize = 3;
        private int zSize = 3;
        private void BluePrintClicked(string ButtonString) //Todo: temp Method
        {
            Debug.Log($"{ButtonString} Clicked.");

            //Find Prefab to Instantiate with string ButtonString.
            //Like => Resources.Load($"Prefab/Rooms/{ButtonString}") as GameObject

            isAttached = true;
            summoned = Instantiate(tempPrefab, cursorManager.mainCursor.transform);
        }
    }
}