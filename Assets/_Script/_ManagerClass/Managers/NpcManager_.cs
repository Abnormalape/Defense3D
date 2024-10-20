using UnityEngine;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class NpcManager_ : MonoBehaviour, IManagerClass
    {
        public GameManager_ GameManager { get; set; }

        public void InitializeManager(GameManager_ gameManager_)
        {
            GameManager = gameManager_;
        }
    }
}
