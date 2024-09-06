using UnityEngine;

namespace BHSSolo.DungeonDefense.Controller
{
    public interface IInputReactor
    {
        public void SubScribeInputManager();

        public void GetKeyDownReaction(KeyCode key); //Todo:
        public void GetKeyReaction(KeyCode key); //Todo:
        public void GetKeyUpReaction(KeyCode key); //Todo:
    }
}
