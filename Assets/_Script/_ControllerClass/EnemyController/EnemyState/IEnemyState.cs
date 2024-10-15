using BHSSolo.DungeonDefense.Controller;

namespace BHSSolo.DungeonDefense.State
{
    public interface IEnemyState
    {
        public EnemyController_ enemyController { get; set; }
        public EnemyStates EnemyState { get; set; }

        public void InitializeEnemyState(EnemyController_ enemyController_);
    }

    public enum EnemyStates
    {
        SearchPath,
        Moving,
        Rest,
    }
}