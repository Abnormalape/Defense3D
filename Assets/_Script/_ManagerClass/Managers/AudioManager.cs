using BHSSolo.DungeonDefense.Controller;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class AudioManager : MonoBehaviour, IManagerClass
    {
        public GameManager_ GameManager { get; set; }


        public void InitializeManager(GameManager_ gameManager_)
        {
            GameManager = gameManager_;
        }
    }
}
