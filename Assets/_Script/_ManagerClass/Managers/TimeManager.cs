using UnityEngine;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class TimeManager : MonoBehaviour, IManagerClass
    {
        public GameManager_ GameManager { get; set; }
        public static float passedTime { get; private set; }

        public void InitializeManager(GameManager_ gameManager_)
        {
            GameManager = gameManager_;
        }

        private void Update()
        {
            passedTime += Time.deltaTime;
        }
    }
}
