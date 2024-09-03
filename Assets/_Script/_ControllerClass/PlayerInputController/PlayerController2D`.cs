using BHSSolo.DungeonDefense.ManagerClass;
using UnityEngine;

namespace BHSSolo.DungeonDefense.Controller
{
    public class PlayerController2D_ : MonoBehaviour, IPlayerController, IController
    {
        public PlayerManager_ playerManager_ { get; set; }


        private void Awake()
        {
            SetPlayerManager();
        }

        public void ControllerInitializer()
        {
            Debug.Log("Player 2D Controller Initialized.");
        }

        public void SetPlayerManager()
        {
            Debug.Log("Set PlayerManager And Add To Manager's Dictionary");
            PlayerManager_ playerManager_ = FindFirstObjectByType<PlayerManager_>();
            playerManager_.AddToDictionary(this, gameObject);
        }

        public void ReactToInput()
        {
            Debug.Log("2D React");
        }
    }
}