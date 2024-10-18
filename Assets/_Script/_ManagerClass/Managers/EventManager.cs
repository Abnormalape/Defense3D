using BHSSolo.DungeonDefense.Controller;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class EventManager : MonoBehaviour, IManagerClass
    {
        public GameManager_ GameManager { get; set; }


        public void InitializeManager(GameManager_ gameManager_)
        {
            GameManager = gameManager_;
        }
    }
}
