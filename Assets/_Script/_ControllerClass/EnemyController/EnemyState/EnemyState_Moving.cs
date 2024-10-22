using BHSSolo.DungeonDefense.Controller;
using BHSSolo.DungeonDefense.Contruct;
using System.Collections.Generic;
using UnityEngine;


namespace BHSSolo.DungeonDefense.State
{
    public class EnemyState_Moving : IState_<EnemyStates, EnemyController_>
    {
        public EnemyStates StateType { get; set; } = EnemyStates.Moving;
        public EnemyController_ BlackBoard { get; set; }

        private Vector3 currentPosition;
        private Vector3 targetPosition;
        private Vector3 aheadVector;
        private Queue<DungeonGridData> path;
        private const float NEAR_TARGET = 0.5f;
        private float EnemySpeed = 10f;


        public void InitializeState(EnemyController_ blackBoard)
        {
            BlackBoard = blackBoard;
        }

        public void StateEnter()
        {
            path = new Queue<DungeonGridData>(BlackBoard.SearchedPath); //Todo: EnemyController 새로 만든다고 길찾기 박살내 놓음
            currentPosition = BlackBoard.transform.position;
            targetPosition = path.Peek().ConstructedPosition;
            aheadVector = (targetPosition - currentPosition).normalized;
            aheadVector.y = 0;
            path.Dequeue();
            Debug.Log("State Moving!");
        }

        public void StateExit()
        {
        }

        float passedTime = 0;
        public void StateUpdate()
        {
            BlackBoard.transform.position += aheadVector * EnemySpeed * Time.deltaTime;

            if (Vector3.Distance(BlackBoard.transform.position, targetPosition) < NEAR_TARGET)
            {
                if (path.Count > 0)
                {
                    currentPosition = BlackBoard.transform.position;
                    targetPosition = path.Peek().ConstructedPosition;

                    aheadVector = (targetPosition - currentPosition).normalized;
                    path.Dequeue();
                }
                else
                {
                    aheadVector = Vector3.zero;
                    passedTime += Time.deltaTime;
                    if (passedTime > 0.3f)
                    {
                        path.Clear();
                        passedTime = 0;
                        BlackBoard.StateMachine.ChangeState(EnemyStates.SearchPath);
                        BlackBoard.ChangeState(EnemyStates.SearchPath);
                    }
                }
            }

        }


    }
}
