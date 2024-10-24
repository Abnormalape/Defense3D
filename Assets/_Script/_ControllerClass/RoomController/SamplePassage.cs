﻿using BHSSolo.DungeonDefense.ManagerClass;
using System.Collections.Generic;
using UnityEngine;

namespace BHSSolo.DungeonDefense.Controller
{
    public class SamplePassage : RoomController, IController
    {
        public IManagerClass OwnerManager { get; set; }
        public override int Room_ID { get; set; }

        public void InitializeController(IManagerClass ownerManager)
        {
            OwnerManager = ownerManager;
            RoomControllerInitializer();
        }

        public override void OnRoomClicked()
        {
            Debug.Log("Sample Passage");
        }

        public override void RoomControllerInitializer()
        {
            base.RoomControllerInitializer();
            Debug.Log("SSamplePassage override parent class's method");
        }
    }
}