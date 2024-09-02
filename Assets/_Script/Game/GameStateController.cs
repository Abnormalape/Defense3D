using BHSSolo.DungeonDefense.Singleton;

namespace BHSSolo.DungeonDefense.Game
{
    public class GameStateController : SingletonMono<GameStateController>
    {
        Function.StateMachineBehaviour GameStateMachine = new();


    }
}
