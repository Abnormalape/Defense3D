using BHSSolo.DungeonDefense.Controller;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class EventManager : MonoBehaviour, IManagerClass
    {
        public GameManager_ OwnerManager { get; set; }


        public void InitializeManager(GameManager_ gameManager_)
        {
            OwnerManager = gameManager_;
        }
    }
}
