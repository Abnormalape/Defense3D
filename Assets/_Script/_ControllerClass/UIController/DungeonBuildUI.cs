using BHSSolo.DungeonDefense.ManagerClass;
using UnityEngine;

namespace BHSSolo.DungeonDefense.Controller
{
    public class DungeonBuildUI : UIController_, IController
    {
        public override UIManager_ UIManager_ { get; set; }
        public override UI_enum UIType { get; set; } = UI_enum.BuildDungeon;
        public override Canvas myCanvas { get; set; }
        public IManagerClass OwnerManager { get; set; }


        private void Update()
        {
            OnUIUpdate();
        }

        public void ControllerInitializer(IManagerClass ownerManager)
        {
            OwnerManager = ownerManager;
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

            Debug.Log("Contruct Dungeon!");
        }
    }
}
