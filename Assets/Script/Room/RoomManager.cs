using BHSSolo.DungeonDefense.Singleton;
using BHSSolo.DungeonDefense.DungeonRoom;
using System.Collections.Generic;
using UnityEngine;

namespace BHSSolo.DungeonDefense.Management
{
    public class RoomManager : SingletonMono<RoomManager>
    {
        [SerializeField] private GameManager GameManager;

        private List<DungeonRoomController> _roomControllers;

        private void Awake()
        {
            if (GameManager == null) { GameManager = GetComponent<GameManager>(); }
        }
    }
}
