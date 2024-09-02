using UnityEngine;
using BHSSolo.DungeonDefense.Singleton;

namespace BHSSolo.DungeonDefense.Management
{
    public class EventManager : SingletonMono<EventManager>
    {
        [SerializeField] DungeonOverLordSceneManager _gameSceneManager;

        public void TitleNewGameButton()
        {
            _gameSceneManager.LoadScene("Dungeon");
        }

        public void TitleLoadGameButton()
        {
            //Go to Game Scene.
        }

        public void TitleOptionButton()
        {
            //Open Option Window.
        }
    }
}