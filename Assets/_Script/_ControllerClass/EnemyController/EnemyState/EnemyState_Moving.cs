using BHSSolo.DungeonDefense.Controller;
using UnityEngine;


namespace BHSSolo.DungeonDefense.State
{
    public class EnemyState_Moving : IState_, IEnemyState
    {
        public EnemyController_ enemyController { get; set; }
        public EnemyStates EnemyState { get; set; } = EnemyStates.Moving;

        public void InitializeEnemyState(EnemyController_ enemyController_)
        {
            enemyController = enemyController_;
        }

        public void StateEnter()
        {
            Debug.Log("Enemy Moving State Enter");
        }

        public void StateExit()
        {
            Debug.Log("Enemy Moving State Exit");
        }

        public void StateUpdate()
        {
            Debug.Log("Enemy Moving State Update");
        }
    }
}
