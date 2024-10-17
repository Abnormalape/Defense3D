﻿using BHSSolo.DungeonDefense.Controller;
using BHSSolo.DungeonDefense.Contruct;
using BHSSolo.DungeonDefense.NPCs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BHSSolo.DungeonDefense.State
{
    public class EnemyState_Moving : IState_<EnemyStates, EnemyController_>, IEnemyState
    {
        public EnemyController_ enemyController { get; set; }
        public EnemyStates EnemyState { get; set; } = EnemyStates.Moving;
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

        public void InitializeEnemyState(EnemyController_ enemyController_)
        {
            enemyController = enemyController_;
        }

        public void StateEnter()
        {
            path = new Queue<DungeonGridData>(enemyController.SearchedPath); //Todo: EnemyController 새로 만든다고 길찾기 박살내 놓음
            currentPosition = enemyController.transform.position;
            targetPosition = path.Peek().ConstructedPosition;
            aheadVector = (targetPosition - currentPosition).normalized;
            aheadVector.y = 0;
            path.Dequeue();
        }

        public void StateExit()
        {
        }

        float passedTime = 0;
        public void StateUpdate()
        {
            enemyController.transform.position += aheadVector * EnemySpeed * Time.deltaTime;

            if (Vector3.Distance(enemyController.transform.position, targetPosition) < NEAR_TARGET)
            {
                if (path.Count > 0)
                {
                    currentPosition = enemyController.transform.position;
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
                        enemyController.StateMachine.ChangeState(EnemyStates.SearchPath);
                        //enemyController.ChangeControllerState(EnemyStates.SearchPath);//Todo: EnemyController 새로 만든다고 길찾기 박살내 놓음
                    }
                }
            }

        }

        
    }
}
