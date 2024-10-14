using BHSSolo.DungeonDefense.Controller;
using BHSSolo.DungeonDefense.Contruct;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;


namespace BHSSolo.DungeonDefense.State
{
    public class EnemyState_Moving : IState_, IEnemyState
    {
        public EnemyController_ enemyController { get; set; }
        public EnemyStates EnemyState { get; set; } = EnemyStates.Moving;

        private Vector3 currentPosition;
        private Vector3 targetPosition;
        private Vector3 aheadVector;
        private Queue<DungeonGridData> path;

        public void InitializeEnemyState(EnemyController_ enemyController_)
        {
            enemyController = enemyController_;
        }

        public void StateEnter()
        {
            Debug.Log("Enemy Moving State Enter");
            Debug.Log(enemyController.SearchedPath.Count);

            path = new Queue<DungeonGridData>(enemyController.SearchedPath);
            currentPosition = enemyController.transform.position;
            targetPosition = path.Peek().ConstructedPosition;
            aheadVector = (targetPosition - currentPosition).normalized;
            aheadVector.y = 0;
            path.Dequeue();
        }

        public void StateExit()
        {
            Debug.Log("Enemy Moving State Exit");
        }

        public void StateUpdate()
        {
            enemyController.transform.position += aheadVector * 3f * Time.deltaTime;

            if (Vector3.Distance(enemyController.transform.position, targetPosition) < 0.1f)
            {
                if (path.Count > 0)
                {
                    currentPosition = targetPosition;
                    targetPosition = path.Peek().ConstructedPosition;

                    aheadVector = (targetPosition - currentPosition).normalized;
                    path.Dequeue();
                }
                else
                {
                    aheadVector = Vector3.zero;
                    enemyController.ChangeControllerState(EnemyStates.SearchPath);
                }
            }

            Debug.Log("Enemy Moving State Update");
        }
    }
}
