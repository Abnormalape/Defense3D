using BHSSolo.DungeonDefense.Controller;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class AudioManager : MonoBehaviour, IManagerClass
    {
        public GameManager_ OwnerManager { get; set; }


        public void InitializeManager(GameManager_ gameManager_)
        {
            OwnerManager = gameManager_;
        }
    }
}
