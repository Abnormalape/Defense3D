using BHSSolo.DungeonDefense.ManagerClass;
using BHSSolo.DungeonDefense.State;
using UnityEngine;

namespace BHSSolo.DungeonDefense.Controller
{
    public class Player2DController_ : MonoBehaviour, IController, IPlayerController
    {
        public PlayerManager_ playerManager_ {  get; set; }

        public IState_ CurrentPlayerState { get; private set; }

        public Transform playerCamera;



        private void Awake()
        {
            if (playerCamera == null)
                playerCamera = Camera.main.transform;
            ControllerInitializer();
        }

        void Start()
        {
            
        }

        private void Update()
        {
            ReactToInput();
        }

        public void ControllerInitializer()
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
            playerManager_ = FindFirstObjectByType<PlayerManager_>();
        }
    }
}
