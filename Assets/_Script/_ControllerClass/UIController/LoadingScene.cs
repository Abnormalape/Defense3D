using BHSSolo.DungeonDefense.ManagerClass;
using UnityEngine;
using UnityEngine.UI;

namespace BHSSolo.DungeonDefense.Controller
{
    public class LoadingScene : UIController_, IController
    {
        public IManagerClass OwnerManager { get; set; }
        public override UIManager_ UIManager_ { get; set; }
        public override UI_enum UIType { get; set; } = UI_enum.LoadScene;
        public override Canvas myCanvas { get; set; }


        [SerializeField] private Image progressBar;
        private bool isOpened = false;
        private SceneManager_ SceneManager_;


        private void Update()
        {
            if (isOpened)
                OnUIUpdate();
        }

        public void ControllerInitializer(IManagerClass ownerManager)
        {
            OwnerManager = ownerManager;
            UIManager_ = (UIManager_)OwnerManager;
            SceneManager_ = OwnerManager.OwnerManager.SceneManager_;

            myCanvas = GetComponent<Canvas>();

            Debug.Log("Loading UI");

            Close();
        }

        public override void FixUI()
        {

        }

        public override void Open()
        {
            myCanvas.enabled = true;
            isOpened = true;
        }
        public override void OnUIUpdate()
        {
            progressBar.fillAmount 
                = SceneManager_.LoadingProgress;
        }

        public override void Close()
        {
            isOpened = false;
            progressBar.fillAmount = 0;
            myCanvas.enabled = false;
        }
    }
}
