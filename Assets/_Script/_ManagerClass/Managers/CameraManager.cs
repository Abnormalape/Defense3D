using UnityEngine;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class CameraManager : MonoBehaviour, IManagerClass
    {
        public GameManager_ OwnerManager { get; set; }

        public void InitializeManager(GameManager_ gameManager_)
        {
            OwnerManager = gameManager_;
        }
    }
}
