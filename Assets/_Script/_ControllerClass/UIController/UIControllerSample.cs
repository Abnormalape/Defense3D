using BHSSolo.DungeonDefense.ManagerClass;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using UnityEngine;

namespace BHSSolo.DungeonDefense.Controller
{
    public class UIControllerSample : UIController_, IController
    {
        public IManagerClass OwnerManager { get; set; }
        public override UIManager_ UIManager_ { get; set; }
        public override UI_enum UIType { get; set; } = UI_enum.Sample;
        public override Canvas myCanvas { get; set; }


        private void Update()
        {
            OnUIUpdate();
        }

        public void InitializeController(IManagerClass ownerManager)
        {
            OwnerManager = ownerManager;
            UIManager_ = (UIManager_)OwnerManager;
            myCanvas = GetComponent<Canvas>();
            Debug.Log("UI Sample");
            Close();
        }
        public override void Open()
        {
            myCanvas.enabled = true;
            //Can Update On here
        }

        public override void Close()
        {
            myCanvas.enabled = false;
        }

        public override void OnUIUpdate()
        {
            if (myCanvas.enabled == false)
                return;

            Debug.Log("SampleUI Updating");
            //Can Update On here
        }

        /// <summary>
        /// use this to fix UI.
        /// </summary>
        public override void FixUI()
        {
            
        }
    }
}