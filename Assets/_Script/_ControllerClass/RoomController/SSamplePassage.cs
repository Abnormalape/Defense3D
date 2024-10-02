﻿using BHSSolo.DungeonDefense.ManagerClass;
using System;
using System.Text;
using UnityEngine;

namespace BHSSolo.DungeonDefense.Controller
{
    public class SSamplePassage : RoomController, IController
    {
        public IManagerClass OwnerManager { get; set; }
        public override int Room_ID { get; set; }

        public void ControllerInitializer(IManagerClass ownerManager)
        {
            OwnerManager = ownerManager;
        }

        public override void RoomControllerInitializer()
        {
            base.RoomControllerInitializer();
            Debug.Log("SSamplePassage override parent class's method");
        }

        protected override void OnAllyEnterEvent(AllyController_ enteredAlly)
        {
            Debug.Log("SSample Passage Event : AllyEnter");
        }

        protected override void OnAllyExitEvent(AllyController_ exitedAlly)
        {
            Debug.Log("SSample Passage Event : AllyExit");
        }

        protected override void OnEnemyEnterEvent(EnemyController_ enteredEnemy)
        {
            Debug.Log("SSample Passage Event : EnemyEnter");
        }

        protected override void OnEnemyExitEvent(EnemyController_ exitedEnemy)
        {
            Debug.Log("SSample Passage Event : EnemyExit");
        }
    }
}