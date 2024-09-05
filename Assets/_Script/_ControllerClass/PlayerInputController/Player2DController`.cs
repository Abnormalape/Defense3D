using BHSSolo.DungeonDefense.ManagerClass;
using BHSSolo.DungeonDefense.State;
using UnityEngine;

namespace BHSSolo.DungeonDefense.Controller
{
    public class Player2DController_ : MonoBehaviour, IController, IPlayerController
    {
        public PlayerManager_ playerManager_ {  get; set; }

        public IState_ CurrentPlayerState { get; private set; }
        public IManagerClass OwnerManager { get; set; }

        public Transform playerCamera;



        private void Awake()
        {
            if (playerCamera == null)
                playerCamera = Camera.main.transform;
            //ControllerInitializer(); //Todo:
        }

        void Start()
        {
            
        }

        private void Update()
        {
            ReactToInput();
        }

        public void ControllerInitializer(IManagerClass owenerManager)
        {
            SetPlayerManager();
        }

        public void ReactToInput()
        {
            if(Input.GetKeyDown(KeyCode.D))
            {
                Debug.Log("2D D Input!");
            }
        }

        public void SetPlayerManager()
        {
            OwnerManager = FindFirstObjectByType<PlayerManager_>();
            playerManager_ = (PlayerManager_)OwnerManager;
        }
    }
}
