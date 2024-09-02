using BHSSolo.DungeonDefense.Singleton;
using UnityEngine.SceneManagement;

namespace BHSSolo.DungeonDefense.Management
{
    public class DungeonOverLordSceneManager : SingletonMono<DungeonOverLordSceneManager>
    {
        public void LoadScene(string changingScene)
        {
            SceneManager.LoadScene(changingScene);
        }
    }
}
