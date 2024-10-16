using BHSSolo.DungeonDefense.ManagerClass;
using UnityEngine;
using UnityEngine.UI;

namespace BHSSolo.DungeonDefense.Controller
{
    public class DungeonManagerUI : UIController_, IController
    {
        public override UIManager_ UIManager_ { get; set; }
        public override UI_enum UIType { get; set; } = UI_enum.Manager;
        public override Canvas myCanvas { get; set; }
        public IManagerClass OwnerManager { get; set; }

        private GameStateManager_ gameStateManager;

        [SerializeField] private Button constructionButton;
        public Button ConstructionButton
        {
            get
            {
                if (constructionButton == null) { constructionButton = transform.Find("ConstructionButton").GetComponent<Button>(); }
                return constructionButton;
            }
        }


        private void Update()
        {
            OnUIUpdate();
        }

        public void InitializeController(IManagerClass ownerManager)
        {
            OwnerManager = ownerManager;
            gameStateManager = OwnerManager.OwnerManager.GameStateManager_;

            constructionButton.onClick.AddListener(() => gameStateManager.ChangeManagerState(GameState.Dungeon_ConstructionState)); //Todo:

            myCanvas = GetComponent<Canvas>();
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

            //Todo: Not in UIController

            if (Input.GetKey(KeyCode.W))
                Camera.main.transform.position += new Vector3(10f, 0, -10f) * Time.deltaTime;
            if (Input.GetKey(KeyCode.S))
                Camera.main.transform.position += new Vector3(-10f, 0, 10f) * Time.deltaTime;
            if (Input.GetKey(KeyCode.A))
                Camera.main.transform.position += new Vector3(10f, 0, 10f) * Time.deltaTime;
            if (Input.GetKey(KeyCode.D))
                Camera.main.transform.position += new Vector3(-10f, 0, -10f) * Time.deltaTime;
        }

        public void OpenConstructUI()
        {
            (OwnerManager as UIManager_).Open(UI_enum.BuildDungeon);
            Close();
        }
    }
}
