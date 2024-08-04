using BHSSolo.DungeonDefense.NPCs;
using BHSSolo.DungeonDefense.Singleton;
using System.Collections.Generic;
using UnityEngine;

namespace BHSSolo.DungeonDefense.Management
{
    public class AllyManager : SingletonMono<AllyManager>
    {
        [SerializeField] private GameManager GameManager;

        private List<AllyController> _allyControllers = new List<AllyController>(200);

        private void Awake()
        {
            if (GameManager == null) { GameManager = GetComponent<GameManager>(); }
        }


    }
}
